using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using PonishaExam.Helper;

namespace PonishaExam.Areas.Student.Controllers
{
    [Area("Student")]
    [StudentAuthorize]
    public class TestController : Controller
    {
        private readonly ITestService _testService;
        private readonly IAnswerService _answerService;
        private readonly IWorkBookService _workBookService;
        private readonly IReportService _report;

        public TestController(ITestService testService,IAnswerService answerService,IWorkBookService workBookService,IReportService report)
        {
            _testService = testService;
            _answerService = answerService;
            _workBookService = workBookService;
            _report = report;
        }
        [HttpGet][Route("/StudentPanel/Test/{lessonId}/{submitAnswerId?}")]
        public IActionResult ShowTests(int lessonId, bool IsFinished, int submitAnswerId,bool testClosed, string search = "", int page = 1)
        {
            int studentId = int.Parse(User.GetStudentId());
            ViewBag.Page = page;
            ViewBag.LessonId = lessonId;
            ViewBag.IsFinished = IsFinished;
            ViewBag.Search = search;
            ViewBag.submitAnswerId = submitAnswerId;
            ViewBag.TestClosed = testClosed;
            var tests = _testService.GetStudentTests(studentId,lessonId,search ?? "",IsFinished, page);
            return View(tests);
        }
        [HttpGet]
        [Route("/StudentPanel/BoughtTests/{submitAnswerId?}")]
        public IActionResult ShowBoughtTests(bool IsFinished, int submitAnswerId, bool testClosed, string search = "", int page = 1)
        {
            int studentId = int.Parse(User.GetStudentId());
            ViewBag.Page = page;
            ViewBag.IsFinished = IsFinished;
            ViewBag.Search = search;
            ViewBag.submitAnswerId = submitAnswerId;
            ViewBag.TestClosed = testClosed;
            var tests = _testService.GetBoughtStudentTests(studentId, search ?? "", IsFinished, page);
            return View(tests);
        }
        [HttpGet]
        [Route("/StudentPanel/Test/EnterTest/{testId}/{lessonId}")]
        public IActionResult EnterTest(int testId,int lessonId)
        {
            ViewBag.LessonId = lessonId;
            int studentId = int.Parse(User.GetStudentId());
            bool countValue = _testService.UpdateEnterCount(testId, studentId).Result;
            if (countValue)
            {
                var test = _testService.GetTestById(testId).Result;
                return View(test);
            }
            else
            {
                bool testClosed = true;
                return RedirectToAction("ShowTests", "Test",new { lessonId = lessonId,testClosed = testClosed });
                
            }
        }
        #region PDF

        [HttpGet]
        [Route("/Student/PrintWorkBookPage/{studentId}/{testId}")]
        public IActionResult PrintWorkBookPage(int studentId, int testId)
        {
            var workBookInfos = _workBookService.ShowWorkBookInfo(studentId, testId);
            var pdfFile = _report.GeneratePdfReport(workBookInfos);
            return File(pdfFile,
                "application/octet-stream", "کارنامه.pdf");
        }


        #endregion
    }
}