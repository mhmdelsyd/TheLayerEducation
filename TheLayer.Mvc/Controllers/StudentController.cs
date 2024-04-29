using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using TheLayer.Core.Helpers.Constants;
using TheLayer.Core.Models.Identities;
using TheLayer.Core.ViewModels;
using TheLayer.InfraStructure.Context;

namespace TheLayer.Mvc.Controllers
{
    public partial class StudentController(SignInManager<Student> signInManger,
        UserManager<Student> userManger,
        TheLayerContext context,
        UserManager<Teacher> userMangerTeacher,
        IEmailSender emailSender) : Controller
    {
        private readonly SignInManager<Student> _signInManger = signInManger;
        private readonly UserManager<Student> _userManger = userManger;
        private readonly UserManager<Teacher> _userMangerTeacher = userMangerTeacher;
        private readonly IEmailSender _emailSender = emailSender;
        private readonly TheLayerContext _context = context;

        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [Authorize(Roles = Roles.Student)]
        public IActionResult GetAllTeachers()
        {
            ViewBag.SubscribeState = SubscribeState.Init;
            if (!User.HasClaim(c => c.Type.Equals("SubscribedTeachers")))
            {
                return View(_context.Teachers.Select(x => new TeacherViewModel
                {
                    Id = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    course = x.Course.Name,
                    Photo = x.UserPhoto,
                    Subscribed = false
                }).AsEnumerable());
            }

            var studentTeachersClaim = User.Claims.Where(c => c.Type == "SubscribedTeachers");
            if (studentTeachersClaim.Any())
            {
                var teachers = _context.Teachers.Select(x => new TeacherViewModel
                {
                    Id = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    course = x.Course.Name,
                    Photo = x.UserPhoto,
                    Subscribed = studentTeachersClaim.Any(c => c.Value == x.Id)
                }).AsEnumerable();
                return View(teachers);
            }

            return View(_context.Teachers.Select(x => new TeacherViewModel
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                course = x.Course.Name,
                Photo = x.UserPhoto,
                Subscribed = false
            }).AsEnumerable());
        }

        [Authorize(Roles = Roles.Student)]
        public async Task<IActionResult> GetAllSubscribedTeachers()
        {
            var student = await _userManger.Users.Include(s => s.SubscribedTeachers)
                .FirstOrDefaultAsync(s => s.UserName.Equals(User.Identity.Name));
            var returnedTeachers = student.SubscribedTeachers.Select(x => new TeacherViewModel
            {
                course = x.Course.Name,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Id = x.Id,
                Photo = x.UserPhoto
            });
            return View(returnedTeachers);
        }

        [Authorize(Roles = Roles.Student)]
        public async Task<IActionResult> GetLessonsSubscribedTeacher(string id)
        {
            var teachers = await _userMangerTeacher.Users.Include(x => x.Lessons)
                .FirstOrDefaultAsync(x => x.Id == id);
            var Lessons = teachers.Lessons.Select(x => new LessonModelView
            {
                Id = x.LessonId,
                LessonName = x.LessonName,
                ImageUrl = x.ImageUrl
            });

            return View(Lessons);
        }

        [Authorize(Roles = Roles.Student)]
        public async Task<IActionResult> GetLesson(Guid id)
        {
            var lesson = await _context.Lessons.FindAsync(id);

            return View(lesson);
        }
    }
}
