using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using PonishaExam.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PonishaExam.Areas.Master.Controllers
{
    [Area("Master")]
    [MasterAuthorize]
    public class WorkBookController : Controller
    {
        private readonly ITestService _testService;
        private readonly IWorkBookService _workBookService;
        private readonly ILevelPercentService _levelPercentService;
        public WorkBookController(ITestService testService, IWorkBookService workBookService, ILevelPercentService levelPercentService)
        {
            _testService = testService;
            _workBookService = workBookService;
            _levelPercentService = levelPercentService;
        }
        [HttpGet]
        [Route("/Master/ShowWorkBooks")]
        public IActionResult Index(string search, int pageNumber = 1)
        {
            ViewBag.Search = search;
            int teacherId = int.Parse(User.GetTeacherId());
            var models = _testService.ShowWorkBooksListForMaster(search ?? "",teacherId, pageNumber);
            return View(models);
        }
        [HttpGet]
        [Route("/Master/ShowSingleWorkBook/{studentId}/{testId}/{testFile?}/{testTitle?}")]
        public IActionResult ShowWorkBook(int studentId, int testId, string testFile, string testTitle)
        {
            var workBookInfos = _workBookService.ShowWorkBookInfo(studentId, testId);
            ViewBag.StudentId = studentId;
            ViewBag.TestId = testId;
            ViewBag.TestFile = testFile;
            ViewBag.TestTitle = testTitle;
            return View(workBookInfos);
        }
        [HttpGet]
        [Route("/Master/WorkBook/WorkBookRefresh/{studentId}/{testId}/{testFile?}/{testTitle?}")]
        public IActionResult WorkBookRefresh(int testId, int studentId, string testFile, string testTitle)
        {
            _levelPercentService.UpdateAllParticipantsInfo(testId);
            return Redirect($"/Master/ShowSingleWorkBook/{studentId}/{testId}/{testFile}/{testTitle}");
        }
    }
}
