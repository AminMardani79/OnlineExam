using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PonishaExam.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PonishaExam.Areas.Student.Controllers
{
    [Area("Student")]
    [StudentAuthorize]
    public class WorkBookController : Controller
    {
        private readonly ITestService _testService;
        private readonly IWorkBookService _workBookService;
        public WorkBookController(ITestService testService,IWorkBookService workBookService)
        {
            _testService = testService;
            _workBookService = workBookService;
        }
        [HttpGet]
        [Route("/StudentPanel/WorkBooks")]
        public IActionResult Index(string search,int page = 1)
        {
            ViewBag.Search = search;
            int studentId = int.Parse(User.GetStudentId());
            var model = _testService.ShowStudentWorkBooksList(search,studentId, page);
            return View(model);
        }
        [HttpGet]
        [Route("/StudentPanel/ShowSingleWorkBook/{studentId}/{testId}/{testFile?}/{testTitle?}")]
        public IActionResult ShowWorkBook(int studentId, int testId, string testFile, string testTitle)
        {
            var workBookInfos = _workBookService.ShowWorkBookInfo(studentId, testId);
            ViewBag.StudentId = studentId;
            ViewBag.TestId = testId;
            ViewBag.TestFile = testFile;
            ViewBag.TestTitle = testTitle;
            return View(workBookInfos);
        }
    }
}
