using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Interfaces;
using Application.ViewModels.StudentViewModel;
using Microsoft.AspNetCore.Mvc.Rendering;
using PonishaExam.Helper;

namespace PonishaExam.Areas.Master.Controllers
{
    [Area("Master")]
    [MasterAuthorize]
    public class StudentController : Controller
    {
        private readonly IStudentService _studentService;
        private readonly IGradeService _gradeService;

        public StudentController(IStudentService studentService, IGradeService gradeService)
        {
            _studentService = studentService;
            _gradeService = gradeService;
        }
        [HttpGet]
        [Route("/MasterPanel/Student")]
        public IActionResult Index(int gradeId,string studentName,int pageNumber = 1)
        {
            Grades();
            ViewBag.StudentName = studentName;
            ViewBag.GradeId = gradeId;
            var models = _studentService.GetStudentListTeacherId(TeacherId(),gradeId,studentName ?? "", pageNumber);
            return View(models);
        }
        [HttpGet]
        [Route("/MasterPanel/Student/AddDeleteList/{id}")]
        public void AddDeleteList(int id)
        {
            _studentService.AddToTrash(id);
        }

        [HttpGet]
        [Route("/MasterPanel/Student/DeleteList")]
        public IActionResult DeleteList(int gradeId, string studentName, int pageNumber = 1)
        {
            Grades();
            ViewBag.StudentName = studentName;
            ViewBag.GradeId = gradeId;
            var model = _studentService.GetDeletedStudentListTeacherId(TeacherId(), gradeId, studentName ?? "", pageNumber);
            return View(model);
        }
        [HttpGet]
        [Route("/MasterPanel/Student/DeleteStudent/{id}")]
        public void DeleteStudent(int id)
        {
            _studentService.DeleteStudent(id);
        }

        [HttpGet]
        [Route("/MasterPanel/Student/RestoreDelete/{id}")]
        public void RestoreDelete(int id)
        {
            _studentService.BackToList(id);
        }
        public void Grades()
        {
            var grades = _gradeService.GetAllGrades().Result;
            ViewBag.Grades = new SelectList(grades, "GradeId", "GradeName");
        }

        public int TeacherId()
        {
            int teacherId = int.Parse(User.GetTeacherId());
            return teacherId;
        }
    }
}
