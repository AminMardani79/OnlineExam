using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Interfaces;
using Application.ViewModels.LessonViewModel;
using Microsoft.AspNetCore.Mvc.Rendering;
using PonishaExam.Helper;

namespace PonishaExam.Areas.Admin.Controllers
{
    [Area("Admin")]
    [AdminAuthorize]
    public class LessonController : Controller
    {
        private readonly ILessonService _lessonService;
        private readonly IGradeService _gradeService;
        public LessonController(ILessonService lessonService,IGradeService gradeService)
        {
            _lessonService = lessonService;
            _gradeService = gradeService;
        }
        [HttpGet]
        [Route("/AdminPanel/Lesson")]
        public IActionResult Index(int? id, string search = "", int pageNumber = 1)
        {
            ViewBag.Search = search;
            ViewBag.GradeId = id;
            var models = _lessonService.GetLessonList(id,search ?? "",pageNumber);
            return View(models);
        }
        [HttpGet]
        [Route("/AdminPanel/Lesson/Create")]
        public IActionResult Create()
        {
            Grades();
            return View();
        }
        [HttpPost]
        [Route("/AdminPanel/Lesson/Create")]
        public IActionResult Create(AddLessonViewModel model)
        {
            if (ModelState.IsValid)
            {
                _lessonService.CreateLesson(model);
                return Redirect("/AdminPanel/Lesson");
            }
            else
            {
                Grades(); return View(model);
            }

        }
        [HttpGet]
        [Route("/AdminPanel/Lesson/Edit/{lessonId}")]
        public IActionResult Edit(int lessonId)
        {
            var lesson = _lessonService.GetLessonId(lessonId).Result;
            if (lesson == null)
            {
                return NotFound();
            }
            Grades();
            return View(lesson);
        }
        [HttpPost]
        [Route("/AdminPanel/Lesson/Edit/{lessonId}")]
        public IActionResult Edit(EditLessonViewModel model)
        {
            if (ModelState.IsValid)
            {
                _lessonService.UpdateLesson(model);
                return Redirect("/AdminPanel/Lesson");
            }
            else
            {Grades();
                return View(model);
            }

        }
        [HttpGet]
        [Route("/AdminPanel/Lesson/AddDeleteList/{id}")]
        public void AddDeleteList(int id)
        {
            _lessonService.AddToTrash(id);
        }

        [HttpGet]
        [Route("/AdminPanel/Lesson/DeleteList")]
        public IActionResult DeleteList(string search = "", int pageNumber = 1)
        {
            ViewBag.Search = search;
            var model = _lessonService.GetDeletedLessonList(search, pageNumber);
            return View(model);
        }
        [HttpGet]
        [Route("/AdminPanel/Lesson/DeleteLesson/{id}")]
        public void DeleteLesson(int id)
        {
            _lessonService.DeleteLesson(id);
        }

        [HttpGet]
        [Route("/AdminPanel/Lesson/RestoreDelete/{id}")]
        public void RestoreDelete(int id)
        {
            _lessonService.BackToList(id);
        }
        public void Grades()
        {
            var grades = _gradeService.GetAllGrades().Result;
            ViewData["GradeId"] = new SelectList(grades, "GradeId", "GradeName");
        }

    }
}
