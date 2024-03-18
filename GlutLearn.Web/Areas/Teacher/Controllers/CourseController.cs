using cloudscribe.Pagination.Models;
using GlutLearn.Services;
using GlutLearn.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace GlutLearn.Web.Areas.Teacher.Controllers
{
    [Area("Teacher")]
    public class CourseController : Controller
    {
        private ICourseInfo _courseInfo;

        public CourseController(ICourseInfo courseInfo)
        {
            _courseInfo = courseInfo;
        }

        public IActionResult Index(int pageNumber = 1, int pageSize = 10)
        {
            return View(_courseInfo.GetAll(pageNumber, pageSize));
        }

        [HttpGet]
        public IActionResult Edit(int id) {
            var viewModel = _courseInfo.GetCourseById(id);
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Edit(CourseInfoViewModel vm) {
            _courseInfo.UpdateCourseInfo(vm);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Create() {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CourseInfoViewModel vm) {
            _courseInfo.InsertCourselInfo(vm);
            return RedirectToAction("Index");   
        }

        public IActionResult Delete(int id)
        {
            _courseInfo.DeleteCourseInfo(id);
            return RedirectToAction("Index");
        }
        
    }
}
