using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Interfaces;
using Application.ViewModels.StudentViewModel;
using Microsoft.AspNetCore.Authorization;
using PonishaExam.Helper;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Html;

namespace PonishaExam.Areas.Student.Controllers
{
    [Area("Student")]
    [StudentAuthorize]
    public class StudentDashboardController : Controller
    {
        private readonly IStudentService _studentService;

        public StudentDashboardController(IStudentService studentService)
        {
            _studentService = studentService;
        }
        [HttpGet]
        [Route("/StudentPanel")]
        public IActionResult Index()
        {
            int studentId = int.Parse(User.GetStudentId());
            var studentDashboard = _studentService.ShowStudentDashboard(studentId);
            double[] scores = studentDashboard.Item3.ToArray();
            string scoreStr = JsonConvert.SerializeObject(scores, Formatting.None);
            ViewBag.Scores = new HtmlString(scoreStr);
            return View(studentDashboard);
        }
        [HttpGet]
        [Route("/StudentPanel/Profile")]
        public IActionResult StudentProfile()
        {
            return View();
        }
        [HttpGet][Route("/StudentPanel/EditProfile")]
        public IActionResult EditProfile()
        {
            int studentId = int.Parse(User.GetStudentId());
            var student = _studentService.GetStudentId(studentId).Result;
            return View(student);
        }
        [HttpPost][Route("/StudentPanel/EditProfile")]
        public IActionResult EditProfile(EditStudentViewModel editStudent)
        {
            if (ModelState.IsValid)
            {
                _studentService.UpdateStudent(editStudent);
                return Redirect("/StudentPanel/Profile");
            }
            else
            {
                return View(editStudent);
            }
        }
    }
}
