using Calendar.Services;
using Calendar.Utility;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Calendar.Controllers
{
    public class LessonController : Controller
    {

        private readonly ILessonService _lessonService;

        public LessonController(ILessonService lessonService)
        {
            _lessonService = lessonService;
        }

        public IActionResult Index()
        {

            ViewBag.Duration = Helper.GetTimeDropDown();
            ViewBag.TeacherList = _lessonService.GetTeacherList();
            ViewBag.StudentList = _lessonService.GetStudentList();

            return View();
        }
    }
}
