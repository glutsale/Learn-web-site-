using GlutLearn.Services;
using GlutLearn.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlutLearn.Web.Areas.Teacher.Controllers
{

    [Area("Teacher")]
    public class LessonController : Controller
    {
        private ILessonInfo _lessonInfo;

        public LessonController(ILessonInfo lessonInfo)
        {
            _lessonInfo = lessonInfo;
        }

        public IActionResult Index(int pageNumber = 1, int pageSize = 10)
        {
            return View(_lessonInfo.GetAll(pageNumber, pageSize));
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var viewModel = _lessonInfo.GetLessonById(id);
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Edit(LessonInfoViewModel vm)
        {
            _lessonInfo.UpdateLessonInfo(vm);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Create()
        {
            var viewModel = new LessonInfoViewModel(_lessonInfo.GetAllCourses());
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Create(LessonInfoViewModel vm)
        {
            _lessonInfo.InsertLessonInfo(vm);
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            _lessonInfo.DeleteLessonInfo(id);
            return RedirectToAction("Index");
        }

    }
}
