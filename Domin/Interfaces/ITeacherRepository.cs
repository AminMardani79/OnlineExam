using Domin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domin.Interfaces
{
    public interface ITeacherRepository
    {
        Task<IEnumerable<TeacherModel>> GetTeachersList(int take, int skip);
        Task<IEnumerable<TeacherModel>> GetDeletedTeachersList(int take, int skip);
        Task<TeacherModel> GetTeacherById(int teacherId);
        Task<TeacherModel> GetDeletedTeacherById(int teacherId);
        Task<IEnumerable<TeacherLessonModel>> GetTeacherLessons(int id);
        void CreateTeacher(TeacherModel teacher);
        void CreateTeacherItems(TeacherLessonModel model);
        void UpdateTeacher(TeacherModel teacher);
        void DeleteTeacher(TeacherModel teacher);
        void DeleteTeacherLesson(TeacherLessonModel model);
        Task<bool> IsExistCode(string code);
        int TeachersCount();
        int DeleteTeachersCount();
        void Save();
        Task<bool> ExistTeacher(string code, string password);
        Task<TeacherModel> GetTeacherByLoginInfo(string code, string password);
        Task<IEnumerable<TeacherModel>> GetTeachersByGradeId(int gradeId);
        void CreateTeacherItem(List<MasterWithStudentModel> masterStudents);
        Task<bool> IsExistMSModel(int studentId,int teacherId);
        Task<bool> ExistUser(string code);
        Task<IEnumerable<MasterWithStudentModel>> GetMasterWithStudent();
    }
}