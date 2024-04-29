using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TheLayer.Core.ViewModels;
using TheLayer.Core.Models.Entities;

namespace TheLayer.Mvc.Controllers
{
    [Authorize(Roles = "Teacher")]
    public partial class TeacherController
    {
        [HttpPost]
        public async Task<IActionResult> EditDetails(EditModelView model)
        {
            var admin = await _userManager.FindByEmailAsync(model.Email);
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
            await _userManager.UpdateAsync(admin);
            await _signInManager.RefreshSignInAsync(admin);
            return RedirectToAction(nameof(EditDetails));
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(LoginModelView loginDto)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(loginDto.Email, loginDto.Password, loginDto.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                    return RedirectToAction("Index", "Home");
            }
            return View(loginDto);
        }
        [HttpPost]
        public async Task<IActionResult> AddLesson(AddLessonModelView model)
        {
            if (!ModelState.IsValid)
                return View(model);
            var Lesson = new Lesson
            {
                LessonId = Guid.NewGuid(),
                LessonName = model.LessonName,
                PdfUrl = model.PdfUrl,
                TeacherId = _userManager.FindByNameAsync(User.Identity.Name).Result.Id,
                VideoUrl = model.VideoUrl,
                ImageUrl = model.ImageUrl,
            };
            await _context.Lessons.AddAsync(Lesson);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(GetAllLessons));
        }
        [HttpPost]
        public async Task<IActionResult> EditLesson(EditLessonModelView model)
        {
            var lesson = await _context.Lessons.FirstOrDefaultAsync(l => l.LessonId == model.Id);
            if (lesson == null)
                return View(model);
            lesson.LessonName = model.LessonName;
            lesson.VideoUrl = lesson.VideoUrl;
            lesson.PdfUrl = lesson.PdfUrl;
            lesson.ImageUrl = model.ImageUrl;
            _context.Lessons.Update(lesson);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(GetLesson), new { Id = lesson.LessonId.ToString() });
        }
    }
}
