using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Application.Interfaces;
using Application.Others;
using Application.ViewModels.GeneralViewModel;
using Application.ViewModels.QuestionViewModel;
using Application.ViewModels.TestViewModel;
using PonishaExam.Helper;
using Microsoft.AspNetCore.Mvc.Rendering;
using Application.ViewModels.CorrectTestViewModel;

namespace PonishaExam.Areas.Master.Controllers
{
    [Area("Master")]
    [MasterAuthorize]
    public class ExamController : Controller
    {
        private readonly IMasterDashboardService _master;
        private readonly IQuestionService _question;
        private readonly ITestService _testService;
        private readonly IQuestionService _questionService;
        private readonly IWorkBookService _workBookService;
        private readonly ILevelPercentService _levelPercentService;
        private readonly IReportService _report;
        private readonly IOrderService _orderService;

        public ExamController(IMasterDashboardService master, IQuestionService question, ITestService testService, IQuestionService questionService
            ,IWorkBookService workBookService,ILevelPercentService levelPercentService,IReportService report,IOrderService orderService)
        {
            _master = master;
            _question = question;
            _testService = testService;
            _questionService = questionService;
            _workBookService = workBookService;
            _levelPercentService = levelPercentService;
            _report = report;
            _orderService = orderService;
        }
        [HttpGet]
        [Route("/Master/Exam")]
        public IActionResult Exams(int page = 1)
        {
            var model = _master.GetTestList(MasterId(), page);
            return View(model);
        }
        [HttpGet]
        [Route("/Master/SearchExam")]
        public IActionResult SearchTest(string IsFinish, string fromDate, string toDate, string testTitle = "", string testCode = "", int page = 1)
        {
            var model = _master.GetTestList(MasterId(), testTitle, testCode, fromDate, toDate, IsFinish, page);
            ViewBag.TestTitle = testTitle;
            ViewBag.TestCode = testCode;
            ViewBag.FromDate = fromDate;
            ViewBag.ToDate = toDate;
            ViewBag.IsFinish = IsFinish;
            return View(model);
        }
        [HttpGet]
        [Route("/Master/Exam/Create")]

        public IActionResult Create()
        {
            ViewBag.teacher = MasterId();
            Lessons();
            return View();
        }

        [HttpPost]
        [Route("/Master/Exam/Create")]
        public IActionResult Create(AddExamViewModel model)
        {
            if (ModelState.IsValid)
            {
                _master.AddExam(model);
                return RedirectToAction(nameof(Exams));
            }
            else
            {
                ViewBag.teacher = MasterId();
                Lessons();
                if (model.LessonId != "0")
                {
                   
                    ViewBag.Name = Grade(model.LessonId);
                }   
                return View(model);
            }
          
        }

        [HttpGet]
        [Route("/Master/Exam/Edit/{id}")]
        public IActionResult Edit(int id)
        {
            Lessons();
            var model = _master.GetExamById(id).Result;
            ViewBag.Name = Grade(model.LessonId);
            return View(model);
        }

        [HttpPost]
        [Route("/Master/Exam/Edit/{id}")]
        public IActionResult Edit(EditExamViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.LessonId != "0")
                {
                    _master.EditExam(model);
                    return RedirectToAction(nameof(Exams));
                }
                else
                {
                    Lessons();
                    return View(model);
                }

            }
            else
            {
                Lessons();
                if (model.LessonId != "0")
                {

                    ViewBag.Name = Grade(model.LessonId);
                }
                return View(model);
            }
        }
        [HttpGet]
        [Route("/Master/Test/DeletedTestList")]
        public IActionResult DeletedTestList(int page = 1)
        {
            var model = _master.GetDeletedTestList(MasterId(),page);
            return View(model);
        }
        [HttpGet]
        [Route("/Master/SearchDeletedTestList")]
        public IActionResult SearchDeletedTestList(string IsFinish, string fromDate, string toDate, string testTitle = "", string testCode = "", int page = 1)
        {
            var model = _master.GetDeletedTestList(MasterId(),testTitle, testCode, fromDate, toDate, IsFinish, page);
            ViewBag.TestTitle = testTitle;
            ViewBag.TestCode = testCode;
            ViewBag.FromDate = fromDate;
            ViewBag.ToDate = toDate;
            ViewBag.IsFinish = IsFinish;
            return View(model);
        }
        public int MasterId()
        {
            int userId = int.Parse(User.GetTeacherId());
            return userId;
        }
        [HttpGet]
        [Route("/Master/Test/AddDeleteList/{id}")]
        public void AddDeleteList(int id)
        {
            _master.AddToTrash(id);
        }
        [HttpGet]
        [Route("/Master/Test/DeleteDataBase/{id}")]
        public void Delete(int id)
        {
            _master.DeleteTest(id);
        }
        [HttpGet]
        [Route("/Master/Test/RestoreDelete/{id}")]
        public void RestoreDelete(int id)
        {
            _master.BackToList(id);
        }
        public void Lessons()
        {
            var id = MasterId();
            ViewBag.lesson = new SelectList(_master.GetLessons(id).Result,"Id","Title");
        }

        [HttpGet]
        [Route("/GetGrade/{id}")]
        public JsonResult GradeId(int id)
        {
            var item = _master.GetGrade(id).Result;
            return Json(item);
        }

        public string Grade(string id)
        {
            var item = _master.GetGrade(Convert.ToInt32(id)).Result;
            return item.Title;
        }
        #region DownloadFile

        [HttpGet]
        [Route("/Master/DownloadFile/{fileName}")]
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

        [HttpGet][Route("/Master/Exam/Question/{id}/{gradeId}/{lessonId}")]
        public IActionResult Question(int id, int gradeId, int lessonId, int page = 1)
        {
            ViewBag.GradeId = gradeId;
            ViewBag.LessonId = lessonId;
            var model = _question.GetAllQuestions(id,page);
            ViewBag.id = id;
            return View(model);
        }

        [HttpGet]
        [Route("/Master/Exam/AddQuestion/{id}/{gradeId}/{lesson}")]
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
        [Route("/Master/Exam/AddQuestion/{id}/{gradeId}/{lesson}")]
        public IActionResult AddQuestion(CreateQuestionViewModel model, int gradeId, int lesson)
        {
            if (ModelState.IsValid)
            {
                _question.AddQuestion(model);
                return Redirect($"/Master/Exam/Question/{model.TestId}/{gradeId}/{lesson}");
            }
            else
            {
                return View(model);
            }
           
        }

        [HttpGet]
        [Route("/Master/Exam/EditQuestion/{id}/{testId}/{gradeId}/{lesson}")]
        public IActionResult EditQuestion(int id, int gradeId, int lesson, int testId)
        {
            bool IsComprehensiveTest = _testService.IsComprehensiveTest(testId).Result;
            FilterLessonList(gradeId, lesson);
            ViewBag.GradeId = gradeId;
            ViewBag.LessonId = lesson;
            ViewBag.Comprehensive = IsComprehensiveTest;
            var model = _question.GetQuestionById(id).Result;
            return View(model);
        }
        [HttpPost]
        [Route("/Master/Exam/EditQuestion/{id}/{testId}/{gradeId}/{lesson}")]
        public IActionResult EditQuestion(EditQuestionViewModel model, int gradeId, int lesson)
        {
            if (ModelState.IsValid)
            {
                _question.EditQuestion(model);
                return Redirect($"/Master/Exam/Question/{model.TestId}/{gradeId}/{lesson}");
            }
            else
            {
                return View(model);
            }

        }

        #region ExamStudents

        [HttpGet]
        [Route("/Master/Exam/AddExamStudents/{testId}/{gradeId}")]
        public IActionResult AddExamStudents(int gradeId, int testId)
        {
            var model = _master.GetExamStudentsById(testId).Result;
            ViewBag.StudentList = _master.GetExamStudents(gradeId).Result;
            return View(model);
        }

        [HttpPost]
        [Route("/Master/Exam/AddExamStudents/{testId}/{gradeId}")]
        public IActionResult AddExamStudents(SelectStudentViewModel testStudents)
        {
            _master.UpdateExamStudents(testStudents);
            return Redirect("/Master/Exam");
        }

        #endregion

        #region KeyTest

        [HttpGet]
        [Route("/Master/Exam/KeyTest/{id}")]
        public IActionResult ShowKeyTest(int id)
        {
            var questions = _testService.GetKeyTestsByTestId(id).Result;
            return View(questions);
        }

        #endregion

        #region participants

        [HttpGet]
        [Route("/Master/Test/participants/{testId}/{testTitle}/{testFile?}/{successAlert?}")]
        public IActionResult ShowParticipants(int testId, string testFile, string testTitle, string successAlert, string search = "", int pageNumber = 1)
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
        [Route("/Master/Test/CorrectTest/{studentId}/{testId}/{testTitle?}/{testFile?}")]
        public IActionResult CorrectTest(int testId, int studentId, string testFile, string[] descriptiveScore, string testTitle)
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
        [Route("/Master/Test/CorrectTest/{studentId}/{testId}/{testTitle?}/{testFile?}")]
        public IActionResult CorrectTest(List<string> parameters, int studentId, int testId, string testFile, string testTitle)
        {
            if (parameters is null)
            {
                return RedirectToAction("CorrectTest", "Test");
            }
            List<CorrectTestViewModel> models = new List<CorrectTestViewModel>();
            int i = 0;
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
                string parameter = parameters[i];
                parameter = parameter.Replace("%", "");
                model.Percent = Convert.ToDouble(parameter); i++;
                model.Score = _questionService.GetQuestionScore(model.LessonName).Result;
                models.Add(model);
            }
            _workBookService.CreateWorkBook(models, testId, studentId);
            int successAlert = 1;
            return Redirect($"/Master/Test/participants/{testId}/{testTitle}/{testFile}/{successAlert}");
        }
        [HttpGet]
        [Route("/Master/Test/EditCorrectTest/{studentId}/{testId}/{testTitle?}/{testFile?}")]
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
        [Route("/Master/Test/EditCorrectTest/{studentId}/{testId}/{testTitle?}/{testFile?}")]
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
            _workBookService.EditWorkBook(models, testId, studentId);
            int successAlert = 1;
            return Redirect($"/Master/Test/participants/{testId}/{testTitle}/{testFile}/{successAlert}");
        }
        [HttpGet]
        [Route("/Master/Test/ShowWorkBook/{studentId}/{testId}/{testTitle?}/{testFile?}")]
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
        [Route("/Master/Test/RefreshParticipants/{testId}/{testTitle}/{testFile?}")]
        public IActionResult RefreshParticipants(int testId, string testFile, string testTitle)
        {
            _levelPercentService.UpdateAllParticipantsInfo(testId);
            return Redirect($"/Master/Test/participants/{testId}/{testTitle}/{testFile}");
        }
        [HttpGet]
        [Route("/Master/Test/WorkBookRefresh/{studentId}/{testId}/{testTitle?}/{testFile?}")]
        public IActionResult WorkBookRefresh(int testId, int studentId, string testFile, string testTitle)
        {
            _levelPercentService.UpdateAllParticipantsInfo(testId);
            return Redirect($"/Master/Test/ShowWorkBook/{studentId}/{testId}/{testTitle}/{testFile}");
        }
        [HttpGet]
        [Route("/Master/EnableWorkBookShowing/{studentId}/{testId}")]
        public void EnableWorkBookShowing(int studentId, int testId)
        {
            _testService.EnableWorkBook(studentId, testId);
        }
        [HttpGet]
        [Route("/Master/EnableWorkBookShowingAll/{testId}")]
        public void EnableWorkBookShowingAll(int testId)
        {
            _testService.EnableAllWorkBooks(testId);
        }
        [HttpGet]
        [Route("/Master/EnableWorkBookShowingAll")]
        public void EnableWorkBookShowingAll()
        {
            int teacherId = int.Parse(User.GetTeacherId());
            _testService.EnableAllMasterWorkBooks(teacherId);
        }
        #endregion

        #region Report

        [Route("/Master/Test/ReportParticipate/{testId}")]
        public IActionResult ReportParticipate(int testId)
        {
            var model = _testService.GetReportOfTest(testId).Result;
            return View(model);
        }

        #endregion


        #region BoughtTests

        [HttpGet]
        [Route("/Master/BoughtTests/{studentId}")]
        public IActionResult ShowBoughtTests(int studentId, string search = "", int pageNumber = 1)
        {
            ViewBag.StudentId = studentId;
            ViewBag.Search = search;
            var tests = _testService.ShowOrdersBasket(studentId, search, pageNumber);
            return View(tests);
        }
        
        #endregion

        #region PDF

        [HttpGet]
        [Route("/Master/PrintWorkBookPage/{studentId}/{testId}")]
        public IActionResult PrintWorkBookPage(int studentId, int testId)
        {
            var workBookInfos = _workBookService.ShowWorkBookInfo(studentId, testId);
            var pdfFile = _report.GeneratePdfReport(workBookInfos);
            return File(pdfFile,
                "application/octet-stream", "کارنامه.pdf");
        }


        #endregion
        void FilterLessonList(int gradeId, int lessonId)
        {
            ViewBag.Lessons = new SelectList(_testService.GetLessonList(gradeId, lessonId), "LessonId", "LessonName");
        }
    }
}
