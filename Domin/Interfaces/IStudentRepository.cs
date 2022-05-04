using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domin.Models;

namespace Domin.Interfaces
{
    public interface IStudentRepository
    {
        Task<IEnumerable<StudentModel>> GetStudentList();
        Task<IEnumerable<StudentModel>> GetFilterStudentList(string search);
        Task<IEnumerable<StudentModel>> GetStudentDeletedList();
        Task<IEnumerable<StudentModel>> GetFilterStudentDeletedList(string search);
        Task<StudentModel> GetStudentById(int studentId);
        Task<StudentModel> GetStudentById(int studentId,string search);
        Task<StudentModel> GetDeletedStudentById(int studentId);
        void Create(StudentModel model);
        void Update(StudentModel model);
        void Delete(StudentModel model);
        Task<int> GetStudentsCount();

        #region Account

        Task<StudentModel> GetStudent(string nationalCode, string password);

        #endregion

        #region Master Panel

        Task<IEnumerable<StudentModel>> GetStudentListByTeacherId(int teacherId);
        Task<IEnumerable<StudentModel>> GetFilterStudentListByTeacherId(int? gradeId, string studentName, int teacherId);
        Task<IEnumerable<StudentModel>> GetStudentDeletedListByTeacherId(int teacherId);
        Task<IEnumerable<StudentModel>> GetFilterStudentDeletedListByTeacherId(int? gradeId, string studentName, int teacherId);
        Task<IEnumerable<StudentModel>> GetStudentsByLessonId(int lessonId);
        Task<bool> IsExistMSModel(int teacherId,int studentId);
        void CreateStudentItem(MasterWithStudentModel msModel);

        #endregion

        Task<bool> ExistUser(string code);
    }
}