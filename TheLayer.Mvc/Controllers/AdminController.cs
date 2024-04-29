
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TheLayer.Core.Helpers.Constants;
using TheLayer.Core.Models.Identities;
using TheLayer.Core.ViewModels;
using TheLayer.InfraStructure.Context;

namespace TheLayer.Controllers
{
    [Authorize(Roles = "Admin")]
    public partial class AdminController : Controller
    {
        public readonly SignInManager<Admin> _signInManger;
        private readonly UserManager<Admin> _userManger;
        private readonly IEmailSender _emailSender;
        private readonly UserManager<Teacher> _userMangerTeacher;
        private readonly TheLayerContext _context;

        public AdminController(SignInManager<Admin> signInManger,
        UserManager<Admin> userManger,
        UserManager<Teacher> userMangerTeacher,
        TheLayerContext context,
        IEmailSender emailSender)
        {
            _signInManger = signInManger;
            _userManger = userManger;
            _emailSender = emailSender;
            _userMangerTeacher = userMangerTeacher;
            _context = context;
        }
        [AllowAnonymous]
        public async Task<IActionResult> Register()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Register(RegisterModelView model)
        {
            if (ModelState.IsValid)
            {
                var Admin = new Admin();
                Admin.Email = model.Email;
                Admin.FirstName = model.FirstName;
                Admin.LastName = model.LastName;
                Admin.UserName = model.Email;
                var result = await _userManger.CreateAsync(Admin, model.Password);
                if (result.Succeeded)
                {
                    await _userManger.AddToRoleAsync(Admin, Roles.Admin);
                    await _signInManger.SignInAsync(Admin, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }
            }

            return RedirectToAction("Register", "Admin");
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginModelView model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManger.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                    return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Login", "Admin");
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManger.SignOutAsync();
            return RedirectToAction("Login", "Admin");
        }

        public IActionResult CreateTeacher()
        {
            ViewBag.courses = _context.Courses;
            return View(new CreateTeacherModelView());
        }

        [HttpPost]
        public async Task<IActionResult> CreateTeacher(CreateTeacherModelView model)
        {
            if (ModelState.IsValid)
            {
                var Teacher = new Teacher();
                string Email = rendom_string() + "@mail.com";
                Teacher.Email = Email;
                Teacher.FirstName = model.FirstName;
                Teacher.LastName = model.LastName;
                Teacher.UserName = Email;
                Teacher.Course = _context.Courses.Find(model.Course);
                var password = rendom_stringPassword();
                var result = await _userMangerTeacher.CreateAsync(Teacher, password);

                if (result.Succeeded)
                {
                    var returnDto = new ConfirmationModelView
                    {
                        success = true,
                        firstName = model.FirstName,
                        lastName = model.LastName,
                        Email = Teacher.Email,
                        pass = password,
                        Course = _context.Courses.Find(model.Course).Name
                    };
                    await _userMangerTeacher.AddToRoleAsync(Teacher, Roles.Teacher);
                    return RedirectToAction("Confirmation", "Admin", returnDto);

                }
            }
            return RedirectToAction("Confirmation", "Admin", new { success = false });
        }

        public IActionResult Confirmation(ConfirmationModelView model)
        {
            return View(model);
        }

        public async Task<IActionResult> GetAllTeachers()
        {
            var teachers = await _userMangerTeacher.Users.Select(x => new ReturnedTeacherModelView
            {
                Course = x.Course.Name,
                Email = x.Email,
                FirstName = x.FirstName,
                LaseName = x.LastName,
                Photo = x.UserPhoto,
            }).ToListAsync();
            return View(teachers);
        }

        public async Task<IActionResult> EditDetails()
        {
            var admin = await _userManger.FindByNameAsync(User.Identity.Name);
            return View(new EditModelView
            {
                Email = admin.Email,
                FirstName = admin.FirstName,
                LastName = admin.LastName,
                Photo = admin.UserPhoto
            });
        }

        [HttpPost]
        public async Task<IActionResult> EditDetails(EditModelView model)
        {
            var admin = await _userManger.FindByEmailAsync(model.Email);
            admin.FirstName = string.IsNullOrEmpty(model.FirstName) ? admin.FirstName : model.FirstName;
            admin.LastName = string.IsNullOrEmpty(model.LastName) ? admin.LastName : model.LastName;

            if (Request.Form.Files.Count > 0)
            {
                var File = Request.Form.Files.FirstOrDefault();

                using (var stream = new MemoryStream())
                {
                    await File.CopyToAsync(stream);
                    admin.UserPhoto = stream.ToArray();
                }
            }
            await _userManger.UpdateAsync(admin);
            await _signInManger.RefreshSignInAsync(admin);
            return RedirectToAction(nameof(EditDetails));
        }

        public IActionResult ResetTeacherPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ResetTeacherPassword(ResetTeacherPasswordModelView model)
        {
            if (!ModelState.IsValid)
                return View(model);
            var teacher = await _userMangerTeacher.Users.Include(t => t.Course).FirstOrDefaultAsync(t => t.Email.Equals(model.TeacherEmail));
            if (teacher == null)
            {
                model.Messages.Add("Teacher Can't Be Found");
                return View(model);
            }

            var tokenReset = await _userMangerTeacher.GeneratePasswordResetTokenAsync(teacher);
            var result = await _userMangerTeacher.ResetPasswordAsync(teacher, tokenReset, model.TeacherPassword);
            if (result.Succeeded)
                return RedirectToAction("Confirmation", new ConfirmationModelView
                {
                    Course = teacher.Course.Name,
                    Email = model.TeacherEmail,
                    firstName = teacher.FirstName,
                    lastName = teacher.LastName,
                    pass = model.TeacherPassword,
                    success = true
                });
            model.Messages.Add("something went Wrong :(");
            return View(model);
        }

        public IActionResult ChangePassword()
        {
            return View(new ResetAdminPasswordModelView());
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ResetAdminPasswordModelView model)
        {
            var admin = await _userManger.FindByNameAsync(User.Identity.Name);
            if (admin == null)
                return View(model);
            var tokenReset = await _userManger.GeneratePasswordResetTokenAsync(admin);
            var result = await _userManger.ResetPasswordAsync(admin, tokenReset, model.AdminPassword);
            if (result.Succeeded)
                return RedirectToAction("Confirmation", new ConfirmationModelView
                {
                    Course = "",
                    Email = admin.Email,
                    firstName = admin.FirstName,
                    lastName = admin.LastName,
                    pass = model.AdminPassword,
                    success = true
                });
            return View(model);
        }

        private string rendom_string()
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var stringChars = new char[8];
            var random = new Random();

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            var finalString = new String(stringChars);
            return finalString;
        }

        private string rendom_stringPassword()
        {
            var charscapital = "ABCDEFGHIJKLMNOPQRSTOVPYXZ";
            var charsmall = charscapital.ToLower();
            var numbers = "1234567890";
            var special = "!@#$%^&*";
            var stringChars = new char[11];
            var random = new Random();

            for (int i = 0; i < 5; i++)
            {
                stringChars[i] = numbers[random.Next(numbers.Length)];
            }
            for (int i = 5; i < 7; i++)
            {
                stringChars[i] = charscapital[random.Next(charscapital.Length)];
            }
            for (int i = 7; i < 9; i++)
            {
                stringChars[i] = charsmall[random.Next(charsmall.Length)];
            }
            for (int i = 9; i < 11; i++)
            {
                stringChars[i] = special[random.Next(special.Length)];
            }

            var finalString = new String(stringChars);
            return finalString;
        }

    }
}
