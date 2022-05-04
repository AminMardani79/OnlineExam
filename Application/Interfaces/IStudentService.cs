using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.ViewModels.AccountViewModel;
using Application.ViewModels.LessonViewModel;
using Application.ViewModels.StudentViewModel;
using Application.ViewModels.TestViewModel;

namespace Application.Interfaces
{
    public interface IStudentService
    {
        Tuple<List<TestsViewModel>,StudentDashboardViewModel, List<double>> ShowStudentDashboard(int studentId);
        Tuple<List<StudentViewModel>, int, int> GetStudentList(string search = "", int page = 1);
        Tuple<List<StudentViewModel>, int, int> GetDeletedStudentList(string search = "", int page = 1);
        Task<EditStudentViewModel> GetStudentId(int studentId);
        Task<EditStudentViewModel> GetDeletedStudentId(int studentId);
        void CreateStudent(AddStudentViewModel model);
        void UpdateStudent(EditStudentViewModel model);
        void DeleteStudent(int studentId);
        void AddToTrash(int studentId);
        void BackToList(int studentId);

        #region Account

        Task<StudentViewModel> LoginStudent(LoginStudentViewModel student);

        #endregion

        #region Master Panel

        Tuple<List<StudentViewModel>, int, int> GetStudentListTeacherId(int teacherId,int gradeId,string studentName, int page = 1);
        Tuple<List<StudentViewModel>, int, int> GetDeletedStudentListTeacherId(int teacherId, int gradeId, string studentName, int page = 1);

        #endregion

        Task<int> GetAuthentication(string code);
    }
}
