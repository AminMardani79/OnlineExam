using Application.ViewModels.TeacherViewModel;
using Domin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ITeacherService
    {
        Task<IEnumerable<TeacherViewModel>> GetTeacherList(int pageId, int take);
        Task<IEnumerable<int>> TeacherLessons(int teacherId);
        Task<IEnumerable<TeacherViewModel>> GetDeletedTeacherList(int pageId, int take);
        Task<EditTeacherViewModel> GetTeacherById(int teacherId);
        Task<TeacherModel> GetDeletedTeacherById(int teacherId);
        void CreateTeacher(AddTeacherViewModel teacher);
        void UpdateTeacher(EditTeacherViewModel teacher);
        void DeleteTeacher(int teacherId);
        void AddToTrash(int teacherId);
        void BackToList(int teacherId);
        bool IsExistCode(string email);
        int TeacherPageCount(int take);
        int DeletedTeachersPageCount(int take);
        Task<bool> ExistTeacher(RequestMasterViewModel model);
        Task<TeacherLoginViewModel> GetTeacherLoginInfo(RequestMasterViewModel model);
    }
}
