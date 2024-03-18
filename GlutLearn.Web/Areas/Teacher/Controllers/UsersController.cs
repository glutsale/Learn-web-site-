using GlutLearn.Services;
using Microsoft.AspNetCore.Mvc;

namespace GlutLearn.Web.Areas.Teacher.Controllers
{
    [Area("Teacher")]
    public class UsersController : Controller
    {

        private ApplicationUserService _userService;

        public UsersController(ApplicationUserService userService)
        {
            _userService = userService;
        }
        public IActionResult Index(int PageNumber = 1, int PageSize = 10)
        {
            return View(_userService.GetAll(PageNumber, PageSize));
        }
    }
}
