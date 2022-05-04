using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Application.Interfaces;
using Application.Others;
using Application.ViewModels.QuestionViewModel;
using Application.ViewModels.TestViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using PonishaExam.Helper;
using Application.ViewModels.CorrectTestViewModel;
using Stimulsoft.Base;
using Stimulsoft.Report;
using Stimulsoft.Report.Mvc;

namespace PonishaExam.Areas.Admin.Controllers
{
    [Area("Admin")]
    [AdminAuthorize]
    public class TestController : Controller
    {
        private readonly ITestService _testService;
        private readonly IQuestionService _questionService;
        private readonly IWorkBookService _workBookService;
        private readonly ILevelPercentService _levelPercentService;
        private readonly IReportService _report;
        private readonly IOrderService _orderService; 

        public TestController(ITestService testService, IQuestionService questionService, IWorkBookService workBookService,
            ILevelPercentService levelPercentService, IReportService report,IOrderService orderService)
        {
            _testService = testService;
            _questionService = questionService;
            _workBookService = workBookService;
            _levelPercentService = levelPercentService;
            _report = report;
            _orderService = orderService;
        }
        [HttpGet]
        [Route("/AdminPanel/Test")]
        public IActionResult Index(int page = 1)
        {
            var model = _testService.GetTestList(page);
            return View(model);
        }
        [HttpGet]
        [Route("/AdminPanel/SearchTest")]
        public IActionResult SearchTest(string IsFinish, string fromDate, string toDate, string testTitle = "", string testCode = "", int page = 1)
        {
            var model = _testService.GetTestList(testTitle ?? "", testCode ?? "", fromDate, toDate, IsFinish, page);
            ViewBag.TestTitle = testTitle;
            ViewBag.TestCode = testCode;
            ViewBag.FromDate = fromDate;
            ViewBag.ToDate = toDate;
            ViewBag.IsFinish = IsFinish;
            return View(model);
        }
        [HttpGet]
        [Route("/AdminPanel/Test/Create")]
        public IActionResult Create()
        {
            Grade();
            return View();
        }
        [HttpPost]
        [Route("/AdminPanel/Test/Create")]
        public IActionResult Create(AddTestViewModel test)
        {
            if (ModelState.IsValid)
            {
                _testService.CreateTest(test);
                return Redirect("/AdminPanel/Test");
            }
            else
            {
                Grade();
                return View(test);
            }
        }
        [HttpGet]
        [Route("/AdminPanel/Test/Edit/{id}")]
        public IActionResult Edit(int id)
        {
            var test = _testService.GetTestById(id).Result;
            Grade();
            LessonList(test.GradeId);
            TeacherList(test.LessonId);
            return View(test);
        }
        [HttpPost]
        [Route("/AdminPanel/Test/Edit/{id}")]
        public IActionResult Edit(EditTestViewModel test)
        {
            if (ModelState.IsValid)
            {
                _testService.EditTest(test);
                return Redirect("/AdminPanel/Test");
            }
            else
            {
                Grade();
                LessonList(test.GradeId);
                TeacherList(test.LessonId);
                return View(test);
            }
        }
        [HttpGet]
        [Route("/AdminPanel/Test/DeletedTestList")]
        public IActionResult DeletedTestList(int page = 1)
        {
            var model = _testService.GetDeletedTestList(page);
            return View(model);
        }
        [HttpGet]
        [Route("/AdminPanel/SearchDeletedTestList")]
        public IActionResult SearchDeletedTestList(string IsFinish, string fromDate, string toDate, string testTitle = "", string testCode = "", int page = 1)
        {
            var model = _testService.GetDeletedTestList(testTitle, testCode, fromDate, toDate, IsFinish, page);
            ViewBag.TestTitle = testTitle;
            ViewBag.TestCode = testCode;
            ViewBag.FromDate = fromDate;
            ViewBag.ToDate = toDate;
            ViewBag.IsFinish = IsFinish;
            return View(model);
        }
        [HttpGet]
        [Route("/AdminPanel/Test/GetLesson/{id}")]
        public JsonResult GetLesson(int id)
        {
            var result = new SelectList(_testService.GetLessonList(id), "LessonId", "LessonName");
            return Json(result);
        }
        [HttpGet]
        [Route("/AdminPanel/Test/GetTeacher/{id}")]
        public JsonResult GetTeacher(int id)
        {
            var result = new SelectList(_testService.GetTeacherList(id), "TeacherId", "TeacherName");
            return Json(result);
        }
        [HttpGet]
        [Route("/AdminPanel/Test/AddDeleteList/{id}")]
        public void AddDeleteList(int id)
        {
            _testService.AddToTrash(id);
        }
        [HttpGet]
        [Route("/AdminPanel/Test/DeleteDataBase/{id}")]
        public void Delete(int id)
        {
            _testService.DeleteTest(id);
        }
        [HttpGet]
        [Route("/AdminPanel/Test/RestoreDelete/{id}")]
        public void RestoreDelete(int id)
        {
            _testService.BackToList(id);
        }
        #region Items

        public void Grade()
        {
            ViewBag.Grades = new SelectList(_testService.GetGradeList(), "ItemGradeId", "ItemGradeName");
        }
        void LessonList(int gradeId)
        {
            ViewBag.Lessons = new SelectList(_testService.GetLessonList(gradeId), "LessonId", "LessonName");
        }

        void FilterLessonList(int gradeId, int lessonId)
        {
            ViewBag.Lessons = new SelectList(_testService.GetLessonList(gradeId, lessonId), "LessonId", "LessonName");
        }
        void TeacherList(int lessonId)
        {
            ViewBag.Teachers = new SelectList(_testService.GetTeacherList(lessonId), "TeacherId", "TeacherName");
        }

        #endregion

        #region DownloadFile

        [HttpGet]
        [Route("/AdminPanel/DownloadFile/{fileName}")]
        public async Task<IActionResult> DownloadFileAsync(string fileName)
        {
            if (fileName == null)
            {
                return null;
            }
            else
            {
                string pathFile = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "files", fileName);
                var memory = new MemoryStream();
                using (var stream = new FileStream(pathFile, FileMode.Open))
                {
                    await stream.CopyToAsync(memory);
                }
                memory.Position = 0;
                return File(memory, DownloadFile.GetContentType(pathFile), Path.GetFileName(pathFile));
            }
        }

        #endregion

        #region Question

        [HttpGet]
        [Route("/AdminPanel/Question/{id}/{gradeId}/{lessonId}")]
        public IActionResult Question(int id, int gradeId, int lessonId, int page = 1)
        {
            ViewBag.GradeId = gradeId;
            ViewBag.LessonId = lessonId;
            var model = _questionService.GetAllQuestions(id, page);
            ViewBag.id = id;
            return View(model);
        }

        [HttpGet]
        [Route("/AdminPanel/Question/Add/{id}/{gradeId}/{lesson}")]
        public IActionResult AddQuestion(int id, int gradeId, int lesson)
        {
            bool IsComprehensiveTest = _testService.IsComprehensiveTest(id).Result;
            FilterLessonList(gradeId, lesson);
            ViewBag.GradeId = gradeId;
            ViewBag.LessonId = lesson;
            ViewBag.Comprehensive = IsComprehensiveTest;
            ViewBag.id = id;
            return View();
        }

        [HttpPost]
        [Route("/AdminPanel/Question/Add/{id}/{gradeId}/{lesson}")]
        public IActionResult AddQuestion(CreateQuestionViewModel model, int gradeId, int lesson)
        {
            if (ModelState.IsValid)
            {
                _questionService.AddQuestion(model);
                return Redirect($"/AdminPanel/Question/{model.TestId}/{gradeId}/{lesson}");
            }
            else
            {
                return View(model);
            }
        }
        [HttpGet]
        [Route("/AdminPanel/Question/Edit/{id}/{testId}/{gradeId}/{lesson}")]
        public IActionResult EditQuestion(int id, int gradeId, int lesson,int testId)
        {
            bool IsComprehensiveTest = _testService.IsComprehensiveTest(testId).Result;
            FilterLessonList(gradeId, lesson);
            ViewBag.GradeId = gradeId;
            ViewBag.LessonId = lesson;
            ViewBag.Comprehensive = IsComprehensiveTest;
            var model = _questionService.GetQuestionById(id).Result;
            return View(model);
        }

        [HttpPost]
        [Route("/AdminPanel/Question/Edit/{id}/{testId}/{gradeId}/{lessonId}")]
        public IActionResult EditQuestion(EditQuestionViewModel model, int gradeId, int lesson)
        {
            if (ModelState.IsValid)
            {
                _questionService.EditQuestion(model);
                return Redirect($"/AdminPanel/Question/{model.TestId}/{gradeId}/{lesson}");
            }
            else
            {
                return View(model);
            }
        }

        [HttpGet]
        [Route("/AdminPanel/Question/Remove/{id}")]
        public void Remove(int id)
        {
            _questionService.Delete(id);
        }
        #endregion

        #region TestStudents

        [HttpGet]
        [Route("/AdminPanel/Test/AddTestStudents/{testId}/{gradeId}")]
        public IActionResult AddTestStudents(int gradeId, int testId)
        {
            var model = _testService.GetTestStudentsById(testId).Result;
            ViewBag.StudentList = _testService.GetTestStudents(gradeId).Result;
            return View(model);
        }

        [HttpPost]
        [Route("/AdminPanel/Test/AddTestStudents/{testId}/{gradeId}")]
        public IActionResult AddTestStudents(SelectStudentViewModel testStudents)
        {
            _testService.UpdateTestStudents(testStudents);
            return Redirect("/AdminPanel/Test");
        }

        #endregion

        #region KeyTest
        [HttpGet]
        [Route("/AdminPanel/Test/KeyTest/{id}")]
        public IActionResult ShowKeyTest(int id)
        {
            var questions = _testService.GetKeyTestsByTestId(id).Result;
            return View(questions);
        }

        #endregion

        #region participants

        [HttpGet]
        [Route("/AdminPanel/Test/participants/{testId}/{testTitle}/{testFile?}/{successAlert?}")]
        public IActionResult ShowParticipants(int testId, string testTitle ,string testFile,string successAlert, string search = "", int pageNumber = 1)
        {
            ViewBag.TestId = testId;
            ViewBag.Search = search;
            ViewBag.TestFile = testFile;
            ViewBag.TestTitle = testTitle;
            ViewBag.SuccessAlert = successAlert;
            var participants = _testService.GetParticipants(testId, search ?? "", pageNumber);
            return View(participants);
        }
        [HttpGet]
        [Route("/AdminPanel/Test/CorrectTest/{studentId}/{testId}/{testTitle?}/{testFile?}")]
        public IActionResult CorrectTest(int testId, int studentId, string testFile, string[] descriptiveScore,string testTitle)
        {
            ViewBag.TestId = testId;
            ViewBag.TestFile = testFile;
            ViewBag.StudentId = studentId;
            ViewBag.TestTitle = testTitle;
            var scoreList = descriptiveScore.ToList();
            var model = _testService.CorrectTest(testId, studentId, scoreList);
            return View(model);
        }
        [HttpPost]
        [Route("/AdminPanel/Test/CorrectTest/{studentId}/{testId}/{testTitle?}/{testFile?}")]
        public IActionResult CorrectTest(List<string> parameters,int studentId, int testId, string testFile, string testTitle)
        {
            if (parameters is null)
            {
                return RedirectToAction("CorrectTest","Test");
            }
            List<CorrectTestViewModel> models = new List<CorrectTestViewModel>();
            int i = 0;
            parameters = parameters.ToList();
            for (int j=0;j<parameters.Count/6;j++)
            {
                CorrectTestViewModel model = new CorrectTestViewModel();
                model.TestId = testId;
                model.StudetnId = studentId;
                model.LessonName = parameters[i]; i++;
                model.TestCounts = int.Parse(parameters[i]); i++;
                model.CorrectAnswers = int.Parse(parameters[i]); i++;
                model.WrongAnswers = int.Parse(parameters[i]); i++;
                model.NoCheckedAnswers = int.Parse(parameters[i]); i++;
                string parameter = parameters[i];
                parameter = parameter.Replace("%", "");
                model.Percent = Convert.ToDouble(parameter); i++;
                model.Score = _questionService.GetQuestionScore(model.LessonName).Result;
                models.Add(model);
            }
            _workBookService.CreateWorkBook(models,testId,studentId);
            int successAlert = 1;
            return Redirect($"/AdminPanel/Test/participants/{testId}/{testTitle}/{testFile}/{successAlert}");
        }
        [HttpGet]
        [Route("/AdminPanel/Test/EditCorrectTest/{studentId}/{testId}/{testTitle?}/{testFile?}")]
        public IActionResult EditCorrectTest(int testId, int studentId, string testFile, string[] descriptiveScore, string testTitle)
        {
            ViewBag.TestId = testId;
            ViewBag.TestFile = testFile;
            ViewBag.StudentId = studentId;
            ViewBag.TestTitle = testTitle;
            var scoreList = descriptiveScore.ToList();
            var model = _testService.CorrectTest(testId, studentId, scoreList);
            return View(model);
        }
        [HttpPost]
        [Route("/AdminPanel/Test/EditCorrectTest/{studentId}/{testId}/{testTitle?}/{testFile?}")]
        public IActionResult EditCorrectTest(List<string> parameters, int studentId, int testId, string testFile, string testTitle)
        {
            if (parameters is null)
            {
                return RedirectToAction("CorrectTest", "Test");
            }
            List<CorrectTestViewModel> models = new List<CorrectTestViewModel>();
            int i = 0;
            double percent = 0;
            parameters = parameters.ToList();
            for (int j = 0; j < parameters.Count / 6; j++)
            {
                CorrectTestViewModel model = new CorrectTestViewModel();
                model.TestId = testId;
                model.StudetnId = studentId;
                model.LessonName = parameters[i]; i++;
                model.TestCounts = int.Parse(parameters[i]); i++;
                model.CorrectAnswers = int.Parse(parameters[i]); i++;
                model.WrongAnswers = int.Parse(parameters[i]); i++;
                model.NoCheckedAnswers = int.Parse(parameters[i]); i++;
                percent = Convert.ToDouble(parameters[i].Replace("%", ""));
                model.Percent = percent; i++;
                model.Score = _questionService.GetQuestionScore(model.LessonName).Result;
                models.Add(model);
            }
            _workBookService.EditWorkBook(models,testId,studentId);
            int successAlert = 1;
            return Redirect($"/AdminPanel/Test/participants/{testId}/{testTitle}/{testFile}/{successAlert}");
        }
        [HttpGet]
        [Route("/AdminPanel/Test/ShowWorkBook/{studentId}/{testId}/{testTitle?}/{testFile?}")]
        public IActionResult ShowWorkBook(int studentId,int testId,string testFile,string testTitle)
        {
            var workBookInfos = _workBookService.ShowWorkBookInfo(studentId,testId);
            ViewBag.StudentId = studentId;
            ViewBag.TestId = testId;
            ViewBag.TestFile = testFile;
            ViewBag.TestTitle = testTitle;
            return View(workBookInfos);
        }
        [HttpGet]
        [Route("/AdminPanel/Test/RefreshParticipants/{testId}/{testTitle}/{testFile?}")]
        public IActionResult RefreshParticipants(int testId,string testFile,string testTitle)
        {
            _levelPercentService.UpdateAllParticipantsInfo(testId);
            return Redirect($"/AdminPanel/Test/participants/{testId}/{testTitle}/{testFile}");
        }
        [HttpGet]
        [Route("/AdminPanel/Test/WorkBookRefresh/{studentId}/{testId}/{testTitle?}/{testFile?}")]
        public IActionResult WorkBookRefresh(int testId,int studentId,string testFile,string testTitle)
        {
            _levelPercentService.UpdateAllParticipantsInfo(testId);
            return Redirect($"/AdminPanel/Test/ShowWorkBook/{studentId}/{testId}/{testTitle}/{testFile}");
        }
        [HttpGet]
        [Route("/AdminPanel/EnableWorkBookShowing/{studentId}/{testId}")]
        public void EnableWorkBookShowing(int studentId,int testId)
        {
            _testService.EnableWorkBook(studentId,testId);
        }
        [HttpGet]
        [Route("/AdminPanel/EnableWorkBookShowingAll/{testId}")]
        public void EnableWorkBookShowingAll(int testId)
        {
            _testService.EnableAllWorkBooks(testId);
        }
        [HttpGet]
        [Route("/AdminPanel/EnableWorkBookShowingAll")]
        public void EnableWorkBookShowingAll()
        {
            _testService.EnableAllWorkBooks();
        }
        #endregion

        #region Report
        [Route("/AdminPanel/Test/ReportParticipate/{testId}")]
        public IActionResult ReportParticipate(int testId)
        {
            var model = _testService.GetReportOfTest(testId).Result;
            return View(model);
        }

        #endregion

        #region BoughtTests

        [Route("/AdminPanel/BoughtTests/{studentId}")]
        public IActionResult ShowBoughtTests(int studentId,string search="",int pageNumber= 1)
        {
            ViewBag.StudentId = studentId;
            ViewBag.Search = search;
            var tests = _testService.ShowOrdersBasket(studentId, search, pageNumber);
            return View(tests);
        }
        [Route("/AdminPanel/RemoveOrder/{orderId}")]
        public void RemoveOrder(int orderId)
        {
            _orderService.RemoveOrderByOrderId(orderId);
        }
        #endregion

        #region PDF

        [HttpGet]
        [Route("/Admin/PrintWorkBookPage/{studentId}/{testId}")]
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