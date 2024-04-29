using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using TheLayer.Core.Helpers.Constants;
using TheLayer.Core.Models.Identities;
using TheLayer.Core.ViewModels;

namespace TheLayer.Mvc.Controllers
{
    public partial class StudentController
    {

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Register(RegisterModelView registerDto)
        {
            if (ModelState.IsValid)
            {
                var student = new Student();
                student.Email = registerDto.Email;
                student.FirstName = registerDto.FirstName;
                student.LastName = registerDto.LastName;
                student.UserName = registerDto.Email;
                var result = await _userManger.CreateAsync(student, registerDto.Password);
                if (result.Succeeded)
                {
                    await _userManger.AddToRoleAsync(student, Roles.Student);
                    await _signInManger.SignInAsync(student, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }
            }
            return View(registerDto);
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(LoginModelView loginDto)
        {
            if (ModelState.IsValid)
            {
                var student = await _userManger.Users.Include(s => s.SubscribedTeachers)
                                        .FirstOrDefaultAsync(s => s.Email.Equals(loginDto.Email));
                var exist = await _signInManger.UserManager.CheckPasswordAsync(student, loginDto.Password);
                if (exist)
                {
                    var customClaims = new List<Claim>();
                    if (student.SubscribedTeachers is not null && student.SubscribedTeachers.Any())
                        foreach (var teacher in student.SubscribedTeachers)
                            customClaims.Add(new Claim("SubscribedTeachers", teacher.Id));
                    if (customClaims.Any())
                        await _signInManger.SignInWithClaimsAsync(student, true, customClaims);

                    var result = await _signInManger.PasswordSignInAsync(student, loginDto.Password, loginDto.RememberMe, lockoutOnFailure: false);
                    if (result.Succeeded)
                        return RedirectToAction("Index", "Home");
                }
            }
            return View(loginDto);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManger.SignOutAsync();
            return RedirectToAction("Login", "Student");
        }

        [HttpPost]
        [Authorize(Roles = Roles.Student)]
        public async Task<IActionResult> SubscribeTeacher(string id)
        {
            var student = await _userManger.Users.Include(s => s.SubscribedTeachers)
                .FirstOrDefaultAsync(s => s.UserName.Equals(User.Identity.Name));
            var teachers = student.SubscribedTeachers;
            if (teachers.Any(x => x.Id == id))
            {
                ViewBag.SubscribeState = SubscribeState.AlreadySubscribed;
                return View();
            }
            teachers.Add(await _userMangerTeacher.FindByIdAsync(id));
            var result = await _userManger.UpdateAsync(student);

            if (result.Succeeded) ViewBag.SubscribeState = SubscribeState.Done;
            else ViewBag.SubscribeState = SubscribeState.Failed;

            return View();
        }
    }
}
