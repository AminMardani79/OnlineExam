using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Interfaces;
using Application.ViewModels.StudentViewModel;
using Microsoft.AspNetCore.Mvc.Rendering;
using PonishaExam.Helper;

namespace PonishaExam.Areas.Admin.Controllers
{
    [Area("Admin")]
    [AdminAuthorize]
    public class StudentController : Controller
    {
        private readonly IStudentService _studentService;
        private readonly IGradeService _gradeService;

        public StudentController(IStudentService studentService,IGradeService gradeService)
        {
            _studentService = studentService;
            _gradeService = gradeService;
        }
        [HttpGet]
        [Route("/AdminPanel/Student/{search?}")]
        public IActionResult Index(string search = "", int pageNumber = 1)
        {
            ViewBag.Search = search;
            var models = _studentService.GetStudentList(search, pageNumber);
            return View(models);
        }
        [HttpGet]
        [Route("/AdminPanel/Student/Create")]
        public IActionResult Create()
        {
            Grades();
            return View();
        }
        [HttpPost]
        [Route("/AdminPanel/Student/Create")]
        public IActionResult Create(AddStudentViewModel model)
        {
            if (ModelState.IsValid)
            {
                _studentService.CreateStudent(model);
                return Redirect("/AdminPanel/Student");
            }
            else
            {
                Grades(); return View(model);
            }
        }
        [HttpGet]
        [Route("/AdminPanel/Student/Edit/{studentId}")]
        public IActionResult Edit(int studentId)
        {
            var lesson = _studentService.GetStudentId(studentId).Result;
            if (lesson == null)
            {
                return NotFound();
            }
            Grades();
            return View(lesson);
        }
        [HttpPost]
        [Route("/AdminPanel/Student/Edit/{studentId}")]
        public IActionResult Edit(EditStudentViewModel model)
        {
            if (ModelState.IsValid)
            {
                _studentService.UpdateStudent(model);
                return Redirect("/AdminPanel/Student");
            }
            else
            {
                Grades();
                return View(model);
            }

        }
        [HttpGet]
        [Route("/AdminPanel/Student/AddDeleteList/{id}")]
        public void AddDeleteList(int id)
        {
            _studentService.AddToTrash(id);
        }

        [HttpGet]
        [Route("/AdminPanel/Student/DeleteList")]
        public IActionResult DeleteList(string search = "", int pageNumber = 1)
        {
            ViewBag.Search = search;
            var model = _studentService.GetDeletedStudentList(search, pageNumber);
            return View(model);
        }
        [HttpGet]
        [Route("/AdminPanel/Student/DeleteStudent/{id}")]
        public void DeleteStudent(int id)
        {
            _studentService.DeleteStudent(id);
        }

        [HttpGet]
        [Route("/AdminPanel/Student/RestoreDelete/{id}")]
        public void RestoreDelete(int id)
        {
            _studentService.BackToList(id);
        }
        public void Grades()
        {
            var grades = _gradeService.GetAllGrades().Result;
            ViewData["GradeId"] = new SelectList(grades, "GradeId", "GradeName");
        }
    }
}