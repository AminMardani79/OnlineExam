using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.ViewModels.AdminDashboardViewModel;
using Application.ViewModels.GeneralViewModel;
using Application.ViewModels.QuestionViewModel;
using Application.ViewModels.TestViewModel;

namespace Application.Interfaces
{
    public interface IMasterDashboardService
    {
        Tuple<List<TestsViewModel>, MasterDashboardViewModel> GetDashboardViewModel(int id);
        Task<EditMasterViewModel> GetMasterInfoById(int id);
        void UpdateProfile(EditMasterViewModel model);
        Tuple<List<ExamMasterViewModel>, int, int> GetTestList(int masterId, int page = 1);
        Tuple<List<ExamMasterViewModel>, int, int> GetTestList(int masterId, string? testTitle, string? testCode, string? fromDate, string? toDate, string IsFinish, int page = 1);
        Tuple<List<ExamMasterViewModel>, int, int> GetDeletedTestList(int masterId, int page = 1);
        Tuple<List<ExamMasterViewModel>, int, int> GetDeletedTestList(int masterId, string? testTitle, string? testCode, string? fromDate, string? toDate, string IsFinish, int page = 1);
        Task<ItemGradeMasterViewModel> GetGrade(int id);
        Task<IEnumerable<ItemLessonsMasterViewModel>> GetLessons(int id);
        void AddExam(AddExamViewModel model);
        Task<EditExamViewModel> GetExamById(int id);
        void EditExam(EditExamViewModel model);
        void AddToTrash(int id);
        void DeleteTest(int id);
        void BackToList(int id);

        #region ExamStudents

        Task<IEnumerable<TestStudentsViewModel>> GetExamStudents(int gradeId);
        Task<SelectStudentViewModel> GetExamStudentsById(int testId);
        void UpdateExamStudents(SelectStudentViewModel model);

        #endregion
        

    }
}
