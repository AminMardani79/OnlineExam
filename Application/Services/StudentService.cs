using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces;
using Application.Others;
using Application.ViewModels.AccountViewModel;
using Application.ViewModels.LessonViewModel;
using Application.ViewModels.StudentViewModel;
using Application.ViewModels.TestViewModel;
using Domin.Interfaces;
using Domin.Models;

namespace Application.Services
{
    public class StudentService:IStudentService
    {
        private readonly IStudentRepository _studentRepository;
        private readonly ITeacherRepository _teacherRepository;
        private readonly ITestRepository _testRepository;
        private readonly IWorkBookRepository _workBookRepository;

        public StudentService(IStudentRepository studentRepository,ITeacherRepository teacherRepository,ITestRepository testRepository,IWorkBookRepository workBookRepository)
        {
            _studentRepository = studentRepository;
            _teacherRepository = teacherRepository;
            _testRepository = testRepository;
            _workBookRepository = workBookRepository;
        }
        public Tuple<List<TestsViewModel>, StudentDashboardViewModel,List<double>> ShowStudentDashboard(int studentId)
        {
            string sDate = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
            string dtNow = DateTime.Now.ToShamsi();
            DateTime date = DateTime.ParseExact(sDate, "yyyy/MM/dd HH:mm:ss", CultureInfo.InvariantCulture);
            DateTime nowDt = DateTime.ParseExact(dtNow, "yyyy/MM/dd", CultureInfo.InvariantCulture);
            List<TestsViewModel> test = new List<TestsViewModel>();
            var list = _testRepository.GetActiveStudentTestList(date, nowDt,studentId).Result;
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
            int activeTestsCount = _testRepository.ActiveStudentTestsCount(date, nowDt,studentId).Result;
            StudentDashboardViewModel model = new StudentDashboardViewModel();
            model.ActiveTestCount = activeTestsCount;
            List<double> scores = new List<double>();
            var scoresModel = _workBookRepository.GetLevelPercentsById(studentId).Result;
            foreach (var score in scoresModel)
            {
                scores.Add(score.TestScore);
            }
            return Tuple.Create(test,model,scores);
        }
        public Tuple<List<StudentViewModel>, int, int> GetStudentList(string search = "", int page = 1)
        {
            var list = _studentRepository.GetFilterStudentList(search).Result.ToList();
            List<StudentViewModel> models = new List<StudentViewModel>();
            int pageNumber = page;
            int pageCount = PageCounts.PageCount(list.Count, 6);
            int skip = (page - 1) * 6;
            var studentList = list.Skip(skip).Take(6).ToList();
            foreach (var item in studentList)
            {
                models.Add(new StudentViewModel()
                {
                    StudentId = item.StudentId,
                    StudentName = item.StudentName,
                    StudentNationalCode = item.StudentNationalCode,
                    StudentImage = item.StudentAvatar,
                    GradeName = item.Grade.GradeName,
                    Active = item.ActiveAccount
                });
            }
            return Tuple.Create(models, pageCount, pageNumber);
        }
        
        public Tuple<List<StudentViewModel>, int, int> GetDeletedStudentList(string search = "", int page = 1)
        {
            var list = _studentRepository.GetFilterStudentDeletedList(search).Result.ToList();
            List<StudentViewModel> models = new List<StudentViewModel>();
            int pageNumber = page;
            int pageCount = PageCounts.PageCount(list.Count, 10);
            int skip = (page - 1) * 10;
            var studentList = list.Skip(skip).Take(10).ToList();
            foreach (var item in studentList)
            {
                models.Add(new StudentViewModel()
                {
                    StudentId = item.StudentId,
                    StudentName = item.StudentName,
                    StudentNationalCode = item.StudentNationalCode,
                    StudentImage = item.StudentAvatar,
                    GradeName = item.Grade.GradeName
                });
            }
            return Tuple.Create(models, pageCount, pageNumber);
        }

        public async Task<EditStudentViewModel> GetStudentId(int studentId)
        {
            var student = await _studentRepository.GetStudentById(studentId);
            EditStudentViewModel edit = new EditStudentViewModel()
            {
                StudentId = student.StudentId,
                StudentName = student.StudentName,
                StudentPathImage = student.StudentAvatar,
                StudentNationalCode = student.StudentNationalCode,
                StudentMail = student.StudentMail,
                StudentPhoneNumber = student.StudentPhoneNumber,
                GradeId = student.GradeId,
                StudentOldPassword = student.StudentPassword,
                Active = student.ActiveAccount
            };
            return edit;
        }

        public async Task<EditStudentViewModel> GetDeletedStudentId(int studentId)
        {
            var student = await _studentRepository.GetDeletedStudentById(studentId);
            EditStudentViewModel edit = new EditStudentViewModel()
            {
                StudentId = student.StudentId,
                StudentName = student.StudentName,
                StudentPathImage = student.StudentAvatar,
                StudentNationalCode = student.StudentNationalCode,
                StudentMail = student.StudentMail,
                StudentPhoneNumber = student.StudentPhoneNumber,
                GradeId = student.GradeId,
                StudentPassword = student.StudentPassword,
                Active = student.ActiveAccount
            };
            return edit;
        }

        public void CreateStudent(AddStudentViewModel model)
        {
            StudentModel student = new StudentModel();
            student.GradeId = model.GradeId;
            student.StudentMail = model.StudentMail;
            student.StudentName = model.StudentName;
            student.ActiveAccount = model.Active;
            student.StudentNationalCode = model.StudentNationalCode;
            student.StudentPassword = HashPassword.Coding(model.StudentPassword);
            student.StudentPhoneNumber = model.StudentPhoneNumber;
            student.RoleId = 3;
            var check = model.StudentAvatar.IsImage();
            if (check)
            {
                student.StudentAvatar = ImageConvertor.SaveImage(model.StudentAvatar);
            }
            else
            {
                student.StudentAvatar = "masterAvatar.png";
            }
            _studentRepository.Create(student);
            var teachers = _teacherRepository.GetTeachersByGradeId(model.GradeId).Result;
            foreach (var teacher in teachers)
            {
                MasterWithStudentModel msModel = new MasterWithStudentModel();
                var IsExistMSModel = _studentRepository.IsExistMSModel(teacher.TeacherId,student.StudentId).Result;
                if (!IsExistMSModel)
                {
                    msModel.TeacherId = teacher.TeacherId;
                    msModel.StudentId = student.StudentId;
                    _studentRepository.CreateStudentItem(msModel);
                }
            }
        }

        public void UpdateStudent(EditStudentViewModel model)
        {
            var student = _studentRepository.GetStudentById(model.StudentId).Result;
            student.GradeId = model.GradeId;
            student.StudentMail = model.StudentMail;
            student.ActiveAccount = model.Active;
            student.StudentName = model.StudentName;
            student.StudentPassword = model.StudentPassword;
            student.StudentPhoneNumber = model.StudentPhoneNumber;
            student.StudentNationalCode = model.StudentNationalCode;
          
            if (model.StudentPassword != null)
            {
                student.StudentPassword = HashPassword.Coding(model.StudentPassword);
            }
            else
            {
                student.StudentPassword = model.StudentOldPassword;
            }
            if (model.StudentAvatar != null)
            {
                var check = model.StudentAvatar.IsImage();
                if (check)
                {
                    ImageConvertor.RemoveImage(student.StudentAvatar);
                    student.StudentAvatar = ImageConvertor.SaveImage(model.StudentAvatar);
                }
            }
            _studentRepository.Update(student);
        }

        public void DeleteStudent(int studentId)
        {
            var student = _studentRepository.GetDeletedStudentById(studentId).Result;
            ImageConvertor.RemoveImage(student.StudentAvatar);
            _studentRepository.Delete(student);
        }
        public void AddToTrash(int studentId)
        {
            var model = _studentRepository.GetStudentById(studentId).Result;
            model.IsStudentDelete = true;
            _studentRepository.Update(model);
        }
        public void BackToList(int studentId)
        {
            var model = _studentRepository.GetDeletedStudentById(studentId).Result;
            model.IsStudentDelete = false;
            _studentRepository.Update(model);
        }
        #region Account

        public async Task<StudentViewModel> LoginStudent(LoginStudentViewModel student)
        {
            var hashPassword = HashPassword.Coding(student.StudentPassword);
            var studetModel = await _studentRepository.GetStudent(student.StudentNationalCode, hashPassword);
            if (studetModel != null)
            {
                StudentViewModel model = new StudentViewModel()
                {
                    StudentPassword = studetModel.StudentPassword,
                    StudentNationalCode = studetModel.StudentNationalCode,
                    StudentMail = studetModel.StudentMail,
                    StudentImage = studetModel.StudentAvatar,
                    Active = studetModel.ActiveAccount,
                    GradeName = studetModel.Grade.GradeName,
                    StudentId = studetModel.StudentId,
                    StudentName = studetModel.StudentName,
                    StudentPhoneNumber = studetModel.StudentPhoneNumber,
                    GradeId = studetModel.GradeId,
                    RoleId = studetModel.RoleId
                };
                return model;
            }
            else
            {
                return null;
            }
        }
        #endregion
        #region Master Panel

        public Tuple<List<StudentViewModel>, int, int> GetStudentListTeacherId(int teacherId, int gradeId, string studentName, int page = 1)
        {
            var list = _studentRepository.GetFilterStudentListByTeacherId(gradeId, studentName, teacherId).Result.ToList();
            List<StudentViewModel> models = new List<StudentViewModel>();
            int pageNumber = page;
            int pageCount = PageCounts.PageCount(list.Count, 6);
            int skip = (page - 1) * 6;
            var studentList = list.Skip(skip).Take(6).ToList();
            foreach (var item in studentList)
            {
                models.Add(new StudentViewModel()
                {
                    StudentId = item.StudentId,
                    StudentName = item.StudentName,
                    StudentNationalCode = item.StudentNationalCode,
                    StudentImage = item.StudentAvatar,
                    GradeName = item.Grade.GradeName,
                    Active = item.ActiveAccount,
                    StudentPhoneNumber = item.StudentPhoneNumber
                });
            }
            return Tuple.Create(models, pageCount, pageNumber);
        }

        public Tuple<List<StudentViewModel>, int, int> GetDeletedStudentListTeacherId(int teacherId, int gradeId, string studentName, int page = 1)
        {
            var list = _studentRepository.GetFilterStudentDeletedListByTeacherId(gradeId, studentName, teacherId).Result.ToList();
            List<StudentViewModel> models = new List<StudentViewModel>();
            int pageNumber = page;
            int pageCount = PageCounts.PageCount(list.Count, 10);
            int skip = (page - 1) * 10;
            var studentList = list.Skip(skip).Take(10).ToList();
            foreach (var item in studentList)
            {
                models.Add(new StudentViewModel()
                {
                    StudentId = item.StudentId,
                    StudentName = item.StudentName,
                    StudentNationalCode = item.StudentNationalCode,
                    StudentImage = item.StudentAvatar,
                    GradeName = item.Grade.GradeName,
                    StudentPhoneNumber = item.StudentPhoneNumber
                });
            }
            return Tuple.Create(models, pageCount, pageNumber);
        }

        public async Task<int> GetAuthentication(string code)
        {
            var student =await _studentRepository.ExistUser(code);
            var teacher = await _teacherRepository.ExistUser(code);
            if (student)
            {
                return 1;
            }
            else if(teacher)
            {
                return 2;
            }

            return 0;
        }

        #endregion
    }


}
