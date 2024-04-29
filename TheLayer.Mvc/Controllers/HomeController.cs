using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TheLayer.Core.Helpers.Constants;

namespace TheLayer.Controllers
{
    [Authorize(Roles = $"{Roles.Admin},{Roles.Student},{Roles.Teacher}")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        [Authorize]
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Privacy()
        {
            return View();
        }
        [AllowAnonymous]
       public IActionResult GetALlTeachers()
        {
            return View();
        }
    }
}
