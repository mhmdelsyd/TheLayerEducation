using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TheLayer.Core.Models.Entities;
using TheLayer.Core.Models.Identities;
using TheLayer.Core.ViewModels;
using TheLayer.InfraStructure.Context;

namespace TheLayer.Mvc.Controllers
{
    [Authorize(Roles = "Teacher")]
    public partial class TeacherController(UserManager<Teacher> userManager, SignInManager<Teacher> signInManager, TheLayerContext context) : Controller
    {
        private readonly UserManager<Teacher> _userManager = userManager;
        private readonly SignInManager<Teacher> _signInManager = signInManager;
        private readonly TheLayerContext _context = context;

        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Admin");
        }

        public async Task<IActionResult> EditDetails()
        {
            var admin = await _userManager.FindByNameAsync(User.Identity.Name);
            return View(new EditModelView
            {
                Email = admin.Email,
                FirstName = admin.FirstName,
                LastName = admin.LastName,
                Photo = admin.UserPhoto
            });
        }

        public async Task<IActionResult> GetAllLessons()
        {
            var teacher = await _userManager.Users.Include(t => t.Lessons).FirstOrDefaultAsync(teacher => teacher.UserName.Equals(User.Identity.Name));
            var lessons = teacher.Lessons.Select(l => new LessonModelView
            {
                Id = l.LessonId,
                LessonName = l.LessonName,
                ImageUrl = l.ImageUrl,
            });
            return View(lessons);
        }

        public async Task<IActionResult> GetLesson(string Id)
        {
            var lesson = await GetLessonById(Guid.Parse(Id));
            if (lesson == null)
                return RedirectToAction(nameof(GetAllLessons));
            return View(lesson);
        }

        public IActionResult AddLesson()
        {
            return View(new AddLessonModelView());
        }

        public async Task<IActionResult> EditLesson(string id)
        {
            var lesson = await GetLessonById(Guid.Parse(id));
            if (lesson == null)
                return RedirectToAction(nameof(GetAllLessons));
            return View(new EditLessonModelView
            {
                Id = Guid.Parse(id),
                LessonName = lesson.LessonName,
                PdfUrl = lesson.PdfUrl,
                VideoUrl = lesson.VideoUrl,
                ImageUrl = lesson.ImageUrl,
            });
        }

        private async Task<Lesson> GetLessonById(Guid id) =>
            await _context.Lessons.FirstOrDefaultAsync(l => l.LessonId == id);
    }
}
