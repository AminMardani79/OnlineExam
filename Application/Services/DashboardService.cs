using Application.Interfaces;
using Application.Others;
using Application.ViewModels.AdminDashboardViewModel;
using Application.ViewModels.TestViewModel;
using Domin.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class DashboardService:IDashboardService
    {
        private readonly IDashboardRepository _dashboardRepository;
        private readonly ITestRepository _testRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly IWorkBookRepository _workBookRepository;
        public DashboardService(IDashboardRepository dashboardRepository,ITestRepository testRepository
            ,IStudentRepository studentRepository,IWorkBookRepository workBookRepository)
        {
            _dashboardRepository = dashboardRepository;
            _testRepository = testRepository;
            _studentRepository = studentRepository;
            _workBookRepository = workBookRepository;
        }
        public Tuple<List<TestsViewModel>, AdminDashboardViewModel> GetActiveTestList()
        {
            string sDate = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
            string dtNow = DateTime.Now.ToShamsi();
            DateTime date = DateTime.ParseExact(sDate, "yyyy/MM/dd HH:mm:ss", CultureInfo.InvariantCulture);
            DateTime nowDt = DateTime.ParseExact(dtNow, "yyyy/MM/dd", CultureInfo.InvariantCulture);
            List<TestsViewModel> test = new List<TestsViewModel>();
            var list = _dashboardRepository.GetActiveTestList(date, nowDt).Result;
            foreach (var item in list)
            {
                var questionCounts = _testRepository.GetAllQuestionCountsById(item.TestId);
                test.Add(new TestsViewModel()
                {
                    TestTitle = item.TestTitle,
                    TestDayTime = item.TestDayTime.ToString("yyyy/MM/dd"),
                    TestId = item.TestId,
                    StartTime = item.StartTest.ToString("HH:mm"),
                    EndTime = item.EndTest.ToString("HH:mm"),
                    TestFile = item.TestFile,
                    TestCode = item.TestCode,
                    TestDuration = item.TestDuration.ToString(),
                    GradeId = item.GradeId,
                    QuestionCounts = questionCounts,
                    LessonId = item.LessonId,
                    TestPrice = item.TestPrice.ToString("#,0")
                });
            }
            double participated = _testRepository.GetParticipatedTest(date, nowDt).Result;
            double notParticipated = _testRepository.GetNotParticipatedTest(date, nowDt).Result;
            double finishedStudents = _testRepository.GetFinishedStudents(date, nowDt).Result;
            double unFinishedStudents = _testRepository.GetUnFinishedStudents(date, nowDt).Result;
            CalculateChartInfos.CalculatePercent(ref participated, ref notParticipated, ref finishedStudents, ref unFinishedStudents);
            int testsCount = _testRepository.GetTestsCount().Result;
            int studentsCount = _studentRepository.GetStudentsCount().Result;
            int workBooksCount = _workBookRepository.GetWorkBooksCount().Result;
            int activeTestsCount = _testRepository.GetActiveTestsCount(date, nowDt).Result;
            AdminDashboardViewModel model = new AdminDashboardViewModel();
            model.TestsCount = testsCount;
            model.StudentsCount = studentsCount;
            model.WorkBooksCount = workBooksCount;
            model.ActiveTestsCount = activeTestsCount;
            model.Participated = participated;
            model.NotParticipated = notParticipated;
            model.FinishedStudents = finishedStudents;
            model.UnFinishedStudents = unFinishedStudents;
            return Tuple.Create(test, model);
        }
    }
}