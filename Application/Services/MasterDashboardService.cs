using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces;
using Application.Others;
using Application.ViewModels.GeneralViewModel;
using Application.ViewModels.TestViewModel;
using Domin.Interfaces;
using Domin.Models;

namespace Application.Services
{
    public class MasterDashboardService : IMasterDashboardService
    {
        private readonly ITeacherRepository _teacherRepository;
        private readonly ITestRepository _testRepository;
        private readonly IDashboardRepository _dashboardRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly IWorkBookRepository _workBookRepository;

        public MasterDashboardService(ITeacherRepository teacherRepository, ITestRepository testRepository
            , IDashboardRepository dashboardRepository, IStudentRepository studentReposiroty, IWorkBookRepository workBookRepository)
        {
            _teacherRepository = teacherRepository;
            _testRepository = testRepository;
            _dashboardRepository = dashboardRepository;
            _studentRepository = studentReposiroty;
            _workBookRepository = workBookRepository;
        }
        public Tuple<List<TestsViewModel>, MasterDashboardViewModel> GetDashboardViewModel(int id)
        {
            string sDate = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
            string dtNow = DateTime.Now.ToShamsi();
            DateTime date = DateTime.ParseExact(sDate, "yyyy/MM/dd HH:mm:ss", CultureInfo.InvariantCulture);
            DateTime nowDt = DateTime.ParseExact(dtNow, "yyyy/MM/dd", CultureInfo.InvariantCulture);
            List<TestsViewModel> test = new List<TestsViewModel>();
            var list = _dashboardRepository.GetActiveTestList(date, nowDt,id).Result;
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
            double participated = _testRepository.GetParticipatedTest(date, nowDt,id).Result;
            double notParticipated = _testRepository.GetNotParticipatedTest(date, nowDt,id).Result;
            double finishedStudents = _testRepository.GetFinishedStudents(date, nowDt,id).Result;
            double unFinishedStudents = _testRepository.GetUnFinishedStudents(date, nowDt,id).Result;
            CalculateChartInfos.CalculatePercent(ref participated, ref notParticipated, ref finishedStudents, ref unFinishedStudents);
            int activeTestsCount = _testRepository.GetActiveTestsCount(date, nowDt,id).Result;
            var model = _teacherRepository.GetTeacherById(id).Result;
            MasterDashboardViewModel master = new MasterDashboardViewModel();
            master.Avatar = model.TeacherImage;
            master.NationalCode = model.NationalCode;
            master.TeacherEmail = model.TeacherEmail;
            master.PhoneNumber = model.PhoneNumber;
            master.TeacherName = model.TeacherName;
            master.About = model.About;
            master.ActiveTestsCount = activeTestsCount;
            master.Participated = participated;
            master.NotParticipated = notParticipated;
            master.FinishedStudents = finishedStudents;
            master.UnFinishedStudents = unFinishedStudents;
            return Tuple.Create(test, master);
        }

        public async Task<EditMasterViewModel> GetMasterInfoById(int id)
        {
            var model = await _teacherRepository.GetTeacherById(id);
            EditMasterViewModel edit = new EditMasterViewModel();
            edit.About = model.About;
            edit.OldPassword = model.Password;
            edit.PhoneNumber = model.PhoneNumber;
            edit.TeacherEmail = model.TeacherEmail;
            edit.TeacherImage = model.TeacherImage;
            edit.TeacherName = model.TeacherName;
            edit.TeacherId = model.TeacherId;
            return edit;
        }

        public void UpdateProfile(EditMasterViewModel model)
        {
            var master = _teacherRepository.GetTeacherById(model.TeacherId).Result;
            master.About = model.About;
            if (model.Password != null)
            {
                master.Password = HashPassword.Coding(model.Password);
            }
            else
            {
                master.Password = model.OldPassword;
            }
            master.PhoneNumber = model.PhoneNumber;
            master.TeacherEmail = model.TeacherEmail;
            master.TeacherName = model.TeacherName;
            if (model.Avatar != null)
            {
                master.TeacherImage = ImageConvertor.SaveImage(model.Avatar);
                ImageConvertor.RemoveImage(model.TeacherImage);
            }
            _teacherRepository.UpdateTeacher(master);
        }

        public Tuple<List<ExamMasterViewModel>, int, int> GetTestList(int masterId, int page = 1)
        {
            List<ExamMasterViewModel> test = new List<ExamMasterViewModel>();
            var examList = _testRepository.GetMasterExams(masterId).Result.ToList();
            int pageNumber = page;
            int pageCount = PageCounts.PageCount(examList.Count, 6);
            int skip = (page - 1) * 6;
            var resultList = examList.Skip(skip).Take(6).ToList();
            foreach (var item in resultList)
            {
                var questionCounts = _testRepository.GetAllQuestionCountsById(item.TestId);
                test.Add(new ExamMasterViewModel()
                {
                    TestTitle = item.TestTitle,
                    TestDayTime = item.TestDayTime.ToString("yyyy/MM/dd"),
                    TestId = item.TestId,
                    StartTime = item.StartTest.ToString("HH:mm"),
                    EndTime = item.EndTest.ToString("HH:mm"),
                    TestCode = item.TestCode,
                    TestFile = item.TestFile,
                    GradeId = item.GradeId,
                    TestDuration = item.TestDuration.ToString(),
                    QuestionCounts = questionCounts,
                    LessonId = item.LessonId,
                    TestPrice = item.TestPrice.ToString("#,0")
                });
            }
            return Tuple.Create(test, pageCount, pageNumber);
        }

        public Tuple<List<ExamMasterViewModel>, int, int> GetTestList(int masterId, string? testTitle, string? testCode,
            string? fromDate, string? toDate, string IsFinish, int page = 1)
        {
            var startDate = Convert.ToDateTime(fromDate);
            var endDate = Convert.ToDateTime(toDate);
            List<ExamMasterViewModel> test = new List<ExamMasterViewModel>();
            var examList = _testRepository.GetMasterExamsByFilter(masterId,testTitle,testCode, startDate, endDate, IsFinish).Result.ToList();
            int pageNumber = page;
            int pageCount = PageCounts.PageCount(examList.Count, 6);
            int skip = (page - 1) * 6;
            var resultList = examList.Skip(skip).Take(6).ToList();
            foreach (var item in resultList)
            {
                var questionCounts = _testRepository.GetAllQuestionCountsById(item.TestId);
                test.Add(new ExamMasterViewModel()
                {
                    TestTitle = item.TestTitle,
                    TestDayTime = item.TestDayTime.ToString("yyyy/MM/dd"),
                    TestId = item.TestId,
                    StartTime = item.StartTest.ToString("HH:mm"),
                    EndTime = item.EndTest.ToString("HH:mm"),
                    TestCode = item.TestCode,
                    TestDuration = item.TestDuration.ToString(),
                    QuestionCounts = questionCounts,
                    TestPrice = item.TestPrice.ToString("#,0")
                });
            }
            return Tuple.Create(test, pageCount, pageNumber);
        }

        public Tuple<List<ExamMasterViewModel>, int, int> GetDeletedTestList(int masterId, int page = 1)
        {
            List<ExamMasterViewModel> test = new List<ExamMasterViewModel>();
            var examList = _testRepository.GetDeletedMasterExams(masterId).Result.ToList();
            int pageNumber = page;
            int pageCount = PageCounts.PageCount(examList.Count, 6);
            int skip = (page - 1) * 6;
            var resultList = examList.Skip(skip).Take(6).ToList();
            foreach (var item in resultList)
            {
                var questionCounts = _testRepository.GetAllQuestionCountsById(item.TestId);
                test.Add(new ExamMasterViewModel()
                {
                    TestTitle = item.TestTitle,
                    TestDayTime = item.TestDayTime.ToString("yyyy/MM/dd"),
                    TestId = item.TestId,
                    StartTime = item.StartTest.ToString("HH:mm"),
                    EndTime = item.EndTest.ToString("HH:mm"),
                    TestCode = item.TestCode,
                    TestDuration = item.TestDuration.ToString(),
                    QuestionCounts = questionCounts,
                    TestPrice = item.TestPrice.ToString("#,0")
                });
            }
            return Tuple.Create(test, pageCount, pageNumber);
        }

        public Tuple<List<ExamMasterViewModel>, int, int> GetDeletedTestList(int masterId, string? testTitle,
            string? testCode, string? fromDate, string? toDate, string IsFinish, int page = 1)
        {
            var startDate = Convert.ToDateTime(fromDate);
            var endDate = Convert.ToDateTime(toDate);
            List<ExamMasterViewModel> test = new List<ExamMasterViewModel>();
            var examList = _testRepository.GetDeletedMasterExamsByFilter(masterId, testTitle, testCode, startDate, endDate, IsFinish).Result.ToList();
            int pageNumber = page;
            int pageCount = PageCounts.PageCount(examList.Count, 6);
            int skip = (page - 1) * 6;
            var resultList = examList.Skip(skip).Take(6).ToList();
            foreach (var item in resultList)
            {
                var questionCounts = _testRepository.GetAllQuestionCountsById(item.TestId);
                test.Add(new ExamMasterViewModel()
                {
                    TestTitle = item.TestTitle,
                    TestDayTime = item.TestDayTime.ToString("yyyy/MM/dd"),
                    TestId = item.TestId,
                    StartTime = item.StartTest.ToString("HH:mm"),
                    EndTime = item.EndTest.ToString("HH:mm"),
                    TestCode = item.TestCode,
                    TestDuration = item.TestDuration.ToString(),
                    QuestionCounts = questionCounts,
                    TestPrice = item.TestPrice.ToString("#,0")
                });
            }
            return Tuple.Create(test, pageCount, pageNumber);
        }
        public async Task<ItemGradeMasterViewModel> GetGrade(int id)
        {
            ItemGradeMasterViewModel grade = new ItemGradeMasterViewModel();
            var gradeId = await _testRepository.GetGradeIdByLessonId(id);
            var model = await _testRepository.GetGradeId(gradeId);
            grade.Title = model.GradeName;
            grade.Id = model.GradeId;
            return grade;
        }

        public async Task<IEnumerable<ItemLessonsMasterViewModel>> GetLessons(int id)
        {
            var list = await _testRepository.GetLessonItemsByTeacherId(id);
            List<ItemLessonsMasterViewModel> lessons = new List<ItemLessonsMasterViewModel>();
            foreach (var item in list)
            {
                var result = await _testRepository.GetLessonId(item);
                lessons.Add(new ItemLessonsMasterViewModel()
                {
                    Id = result.LessonId,
                    Title = result.LessonName
                });
            }

            return lessons;
        }

        public void AddExam(AddExamViewModel model)
        {
            Random random = new Random();
            TestModel test = new TestModel();
            test.LessonId = Convert.ToInt32(model.LessonId);
            test.GradeId = Convert.ToInt32(model.GradeId);
            test.TestTitle = model.TestTitle;
            test.EndTest = Convert.ToDateTime(model.EndTest);
            test.StartTest = Convert.ToDateTime(model.StartTest);
            test.TestDuration = TimeSpan.Parse(model.TestDuration);
            test.TeacherId = model.TeacherId;
            test.TestCode = random.Next(100000, 999999).ToString();
            test.TestDayTime = Convert.ToDateTime(model.TestDayTime);
            test.TestDescription = model.TestDescription;
            test.IsComprehensiveTest = model.IsComprehensiveTest;
            test.NegativePoint = model.IsComprehensiveTest;
            test.TestPrice = model.TestPrice;
            if (model.TestFile != null)
            {
                test.TestFile = FileConvertor.SaveFile(model.TestFile);
            }
            _testRepository.CreateTest(test);

        }

        public async Task<EditExamViewModel> GetExamById(int id)
        {
            var exam = await _testRepository.GetTestById(id);
            EditExamViewModel edit = new EditExamViewModel();
            edit.LessonId = exam.LessonId.ToString();
            edit.GradeId = exam.GradeId.ToString();
            edit.Id = exam.TestId;
            edit.EndTest = exam.EndTest.PersianTime();
            edit.StartTest = exam.StartTest.PersianTime();
            edit.TeacherId = exam.TeacherId;
            edit.FilePath = exam.TestFile;
            edit.TestDayTime = exam.TestDayTime.ToString("yyyy/MM/dd");
            edit.TestDescription = exam.TestDescription;
            edit.TestTitle = exam.TestTitle;
            edit.TestDuration = exam.TestDuration.ToString();
            edit.IsComprehensiveTest = exam.IsComprehensiveTest;
            edit.NegativePoint = exam.NegativePoint;
            edit.TestPrice = exam.TestPrice;
            return edit;
        }

        public void EditExam(EditExamViewModel model)
        {
            var exam = _testRepository.GetTestById(model.Id).Result;
            exam.LessonId = Convert.ToInt32(model.LessonId);
            exam.GradeId = Convert.ToInt32(model.GradeId);
            exam.TestTitle = model.TestTitle;
            exam.EndTest = Convert.ToDateTime(model.EndTest);
            exam.StartTest = Convert.ToDateTime(model.StartTest);
            exam.TestDayTime = Convert.ToDateTime(model.TestDayTime);
            exam.TeacherId = model.TeacherId;
            exam.TestDescription = model.TestDescription;
            exam.TestDuration = TimeSpan.Parse(model.TestDuration);
            exam.IsComprehensiveTest = model.IsComprehensiveTest;
            exam.NegativePoint = model.NegativePoint;
            exam.TestPrice = model.TestPrice;
            if (model.TestFile != null)
            {
                if (model.FilePath != null)
                {
                    FileConvertor.RemoveFile(model.FilePath);
                }

                exam.TestFile = FileConvertor.SaveFile(model.TestFile);
            }
            _testRepository.EditTest(exam);
        }
        public void AddToTrash(int id)
        {
            var test = _testRepository.GetTestById(id).Result;
            test.IsDelete = true;
            _testRepository.EditTest(test);
        }

        public void DeleteTest(int id)
        {
            var test = _testRepository.GetTestById(id).Result;
            _testRepository.RemoveTest(test);
        }
        public void BackToList(int id)
        {
            var test = _testRepository.GetTestById(id).Result;
            test.IsDelete = false;
            _testRepository.EditTest(test);
        }

        #region EmamStudents
        public async Task<IEnumerable<TestStudentsViewModel>> GetExamStudents(int gradeId)
        {
            var testStudents = await _testRepository.GetTestStudents(gradeId);
            List<TestStudentsViewModel> model = new List<TestStudentsViewModel>();
            foreach (var student in testStudents)
            {
                model.Add(new TestStudentsViewModel()
                {
                    StudentId = student.StudentId,
                    StudentName = student.StudentName
                });
            }

            return model;
        }

        public async Task<SelectStudentViewModel> GetExamStudentsById(int testId) 
        {
            var testStudents = await _testRepository.GetTestStudentById(testId);
            SelectStudentViewModel model = new SelectStudentViewModel();
            List<int> items = new List<int>();
            foreach (var student in testStudents)
            {
                items.Add(student.StudentId);
                model.TestId = student.TestId;
                model.items = items;
            }
            return model;
        }
        public void UpdateExamStudents(SelectStudentViewModel model)
        {
            var students = _testRepository.GetTestStudentById(model.TestId).Result;
            foreach (var student in students)
            {
                _testRepository.RemoveTestStudent(student);
            }
            if (model.items != null)
            {
                foreach (var studentId in model.items)
                {
                    TestStudentsModel testStudent = new TestStudentsModel();
                    testStudent.StudentId = studentId;
                    testStudent.TestId = model.TestId;
                    _testRepository.AddTestStudents(testStudent);
                }
            }
        }

        #endregion
    }
}