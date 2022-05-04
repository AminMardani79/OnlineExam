using Application.Interfaces;
using Application.ViewModels.TeacherViewModel;
using Domin.Interfaces;
using Domin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Others;

namespace Application.Services
{
    public class TeacherService : ITeacherService
    {
        private readonly ITeacherRepository _teacherRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly ITestRepository _testService;
        public TeacherService(ITeacherRepository teacherRepository,IStudentRepository studentRepository,ITestRepository testService)
        {
            _teacherRepository = teacherRepository;
            _studentRepository = studentRepository;
            _testService = testService;
        }

        public async Task<IEnumerable<TeacherViewModel>> GetTeacherList(int pageId, int take)
        {
            int pageCount = TeacherPageCount(take);
            int skip = (pageId - 1) * take;
            var list = await _teacherRepository.GetTeachersList(take,skip);
            List<TeacherViewModel> models = new List<TeacherViewModel>();
            foreach (var item in list)
            {
                models.Add(new TeacherViewModel()
                {
                    TeacherName = item.TeacherName,
                    TeacherEmail = item.TeacherEmail,
                    PhoneNumber = item.PhoneNumber,
                    TeacherImage = item.TeacherImage,
                    NationalCode = item.NationalCode,
                    TeacherId = item.TeacherId,
                    CurrentPage = pageId,
                    PageCount = pageCount
                });
            }
            return models;
        }
        public async Task<IEnumerable<int>> TeacherLessons(int teacherId)
        {
            var list = await _teacherRepository.GetTeacherLessons(teacherId);
            List<int>models=new List<int>();
            foreach (var item in list)
            {
                models.Add(item.LessonId);

            }
            return models;
        }

        public async Task<IEnumerable<TeacherViewModel>> GetDeletedTeacherList(int pageId, int take)
        {
            int pageCount = DeletedTeachersPageCount(take);
            int skip = (pageId - 1) * take;
            var list = await _teacherRepository.GetDeletedTeachersList(take, skip);
            List<TeacherViewModel> models = new List<TeacherViewModel>();
            foreach (var item in list)
            {
                int testsCount = _testService.GetTestCountsByTeacherId(item.TeacherId).Result;
                models.Add(new TeacherViewModel()
                {
                    TeacherName = item.TeacherName,
                    TeacherEmail = item.TeacherEmail,
                    PhoneNumber = item.PhoneNumber,
                    TeacherImage = item.TeacherImage,
                    NationalCode = item.NationalCode,
                    TeacherId = item.TeacherId,
                    CurrentPage = pageId,
                    PageCount = pageCount,
                    TestsCount = testsCount
                });
            }
            return models;
        }

        public async Task<EditTeacherViewModel> GetTeacherById(int teacherId)
        {
            var teacher = await _teacherRepository.GetTeacherById(teacherId);
            EditTeacherViewModel model = new EditTeacherViewModel();
            model.OldPassword = teacher.Password;
            model.TeacherId = teacher.TeacherId;
            model.TeacherName = teacher.TeacherName;
            model.TeacherEmail = teacher.TeacherEmail;
            model.OldPassword = teacher.Password;
            model.PhoneNumber = teacher.PhoneNumber;
            model.OldMail = teacher.TeacherEmail;
            model.TeacherImage = teacher.TeacherImage;
            model.NationalCode = teacher.NationalCode;
            model.IsTeacherDeleted = teacher.IsTeacherDeleted;
            model.ActiveAccount = teacher.ActiveAccount;
            model.About = teacher.About;
            return model;
        }
        public async Task<TeacherModel> GetDeletedTeacherById(int teacherId)
        {
            return await _teacherRepository.GetDeletedTeacherById(teacherId);
        }
        public void CreateTeacher(AddTeacherViewModel teacher)
        {
            TeacherModel model = new TeacherModel();
            model.TeacherName = teacher.TeacherName;
            model.ActiveAccount = teacher.ActiveAccount;
            model.TeacherEmail = teacher.TeacherEmail;
            model.About = teacher.About;
            model.Password = HashPassword.Coding(teacher.Password);
            model.PhoneNumber = teacher.PhoneNumber;
            model.RoleId = 2;
            if (teacher.Avatar != null)
            {
                var checkImage = teacher.Avatar.IsImage();
                if (checkImage)
                {
                    model.TeacherImage = ImageConvertor.SaveImage(teacher.Avatar);
                }

            }
            else
            {
                model.TeacherImage = "masterAvatar.png";
            }
            model.NationalCode = teacher.NationalCode;
            _teacherRepository.CreateTeacher(model);
            if (teacher.Items.Count != 0)
            {
                foreach (var item in teacher.Items)
                {
                    TeacherLessonModel itemModel = new TeacherLessonModel()
                    {
                        LessonId = item,
                        TeacherId = model.TeacherId
                    };
                    _teacherRepository.CreateTeacherItems(itemModel);
                }
            }
            foreach (var item in teacher.Items)
            {
                List<MasterWithStudentModel> msModels = new List<MasterWithStudentModel>();
                var students = _studentRepository.GetStudentsByLessonId(item).Result;
                foreach (var student in students)
                {
                    var IsExistMSModel = _teacherRepository.IsExistMSModel(student.StudentId,model.TeacherId).Result;
                    if (!IsExistMSModel)
                    {
                        msModels.Add(new MasterWithStudentModel()
                        {
                            TeacherId = model.TeacherId,
                            StudentId = student.StudentId
                        });
                    }
                }
                _teacherRepository.CreateTeacherItem(msModels);
            }
        }

        public void DeleteTeacher(int teacherId)
        {
            var model = GetDeletedTeacherById(teacherId).Result;
            _teacherRepository.DeleteTeacher(model);
        }
        public void UpdateTeacher(EditTeacherViewModel teacher)
        {
            var model = _teacherRepository.GetTeacherById(teacher.TeacherId).Result;
            model.TeacherName = teacher.TeacherName;
            if (teacher.Password!=null)
            {
                model.Password = HashPassword.Coding(teacher.Password);
            }
            else
            {
                model.Password = teacher.OldPassword;
            }
            model.TeacherEmail = teacher.TeacherEmail;
            model.About = teacher.About;
            model.ActiveAccount = teacher.ActiveAccount;
            model.PhoneNumber = teacher.PhoneNumber;
           
            if (teacher.Avatar != null)
            {
                var check = teacher.Avatar.IsImage();
                if (check)
                {
                    ImageConvertor.RemoveImage(teacher.TeacherImage);
                    model.TeacherImage = ImageConvertor.SaveImage(teacher.Avatar);
                }
            }

            var list = _teacherRepository.GetTeacherLessons(teacher.TeacherId).Result;
            foreach (var item in list)
            {
                _teacherRepository.DeleteTeacherLesson(item);
            }
            if (teacher.Items != null)
            {
                foreach (var item in teacher.Items)
                {
                    TeacherLessonModel itemModel = new TeacherLessonModel()
                    {
                        LessonId = item,
                        TeacherId = model.TeacherId
                    };
                    _teacherRepository.CreateTeacherItems(itemModel);
                }
            }
            model.NationalCode = teacher.NationalCode;
            model.IsTeacherDeleted = teacher.IsTeacherDeleted;
            _teacherRepository.UpdateTeacher(model);
        }
        public void AddToTrash(int teacherId)
        {
            var model = _teacherRepository.GetTeacherById(teacherId).Result;
            model.IsTeacherDeleted = true;
            _teacherRepository.UpdateTeacher(model);
        }

        public void BackToList(int teacherId)
        {
            var model = _teacherRepository.GetDeletedTeacherById(teacherId).Result;
            model.IsTeacherDeleted = false;
            _teacherRepository.UpdateTeacher(model);
        }
        public bool IsExistCode(string email)
        {
            return _teacherRepository.IsExistCode(email).Result;
        }
        
        public int TeacherPageCount(int take)
        {
            int count = _teacherRepository.TeachersCount();
            return PageCount(count, take);
        }
        public int DeletedTeachersPageCount(int take)
        {
            int count = _teacherRepository.DeleteTeachersCount();
            return PageCount(count,take);
        }

        public async Task<bool> ExistTeacher(RequestMasterViewModel model)
        {
            var hash = HashPassword.Coding(model.Password);
            var result = await _teacherRepository.ExistTeacher(model.Code, hash);
            return result;
        }

        public async Task<TeacherLoginViewModel> GetTeacherLoginInfo(RequestMasterViewModel model)
        {
            var hash = HashPassword.Coding(model.Password);
            var result = await _teacherRepository.GetTeacherByLoginInfo(model.Code, hash);
            if (result != null)
            {
                TeacherLoginViewModel login=new TeacherLoginViewModel();
                login.TeacherImage = result.TeacherImage;
                login.TeacherName = result.TeacherName;
                login.TeacherId = result.TeacherId;
                login.TeacherEmail = result.TeacherEmail;
                login.RoleId = result.RoleId;
                login.NationalCode = result.NationalCode;
                return login;
            }
            else
            {
                return null;
            }
        }

        public int PageCount(int count,int take)
        {
            int pageCount = count / take;
            if (count % take != 0)
            {
                pageCount++;
            }

            return pageCount;
        }
    }
}