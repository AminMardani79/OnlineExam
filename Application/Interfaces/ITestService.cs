using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.ViewModels.AnswerViewModel;
using Application.ViewModels.CorrectTestViewModel;
using Application.ViewModels.OrderViewModel;
using Application.ViewModels.QuestionViewModel;
using Application.ViewModels.TeacherViewModel;
using Application.ViewModels.TestViewModel;
using Application.ViewModels.WorkBookViewModel;
using Domin.Models;

namespace Application.Interfaces
{
    public interface ITestService
    {
        Tuple<List<TestsViewModel>, int, int> GetTestList(int page=1);
        Tuple<List<TestsViewModel>, int, int> GetTestList(string? testTitle, string? testCode, string? fromDate, string? toDate, string IsFinish, int page=1);
        Tuple<List<TestsViewModel>, int, int> GetDeletedTestList(int page=1);
        Tuple<List<TestsViewModel>, int, int> GetDeletedTestList(string? testTitle, string? testCode, string? fromDate, string? toDate, string IsFinish, int page = 1);
        Tuple<List<TestsViewModel>, int, int> GetBoughtStudentTests(int studentId,string search, bool IsFinished, int page = 1);
        Task<EditTestViewModel> GetTestById(int testId);
        List<ItemGradeViewModel> GetGradeList();
        List<ItemLessonViewModel> GetLessonList(int gradeId);
        List<ItemLessonViewModel> GetLessonList(int gradeId,int lessonId);
        List<ItemTeacherViewModel> GetTeacherList(int lessonId);
        void CreateTest(AddTestViewModel test);
        void EditTest(EditTestViewModel test);
        void AddToTrash(int testId);
        void BackToList(int testId);
        void DeleteTest(int testId);
        Task<IEnumerable<TestStudentsViewModel>> GetTestStudents(int gradeId);
        Task<SelectStudentViewModel> GetTestStudentsById(int testId);
        void UpdateTestStudents(SelectStudentViewModel model);
        Tuple<List<TestsViewModel>, int, int> GetStudentTests(int studentId, int lessonId, string search, bool IsFinished, int page = 1);
        Task<List<KeyTestViewModel>> GetKeyTestsByTestId(int testId);
        Task<bool> UpdateEnterCount(int testId,int studentId);

        Tuple<List<ParticipantsViewModel>,int,int> GetParticipants(int testId, string search = "", int page = 1);
        Tuple<List<AnswerViewModel>, List<AnswerViewModel>, List<QuestionViewModel>, List<QuestionViewModel>,string, List<CorrectTestViewModel>> CorrectTest(int testId,int studentId,List<string>? descriptiveScore);
        Task<bool> IsComprehensiveTest(int testId);
        void EnableWorkBook(int studentId,int testId);
        void EnableAllWorkBooks(int testId);
        void EnableAllWorkBooks();
        void EnableAllMasterWorkBooks(int teacherId);
        Tuple<List<ShowWorkBooksViewModel>, int, int> ShowWorkBooksList(string search, int page = 1);
        Tuple<List<ShowWorkBooksViewModel>, int, int> ShowWorkBooksListForMaster(string search, int teacherId, int page = 1);
        Task<ReportTestViewModel> GetReportOfTest(int testId);

        #region StudentPanel WorkBooks

        Tuple<List<ShowWorkBooksViewModel>, int, int> ShowStudentWorkBooksList(string search,int studentId, int page = 1);

        #endregion

        #region BasketOrders

        Tuple<List<OrdersBasketViewModel>, int, int> ShowOrdersBasket(int studentId, string search, int page = 1);

        #endregion
    }
}