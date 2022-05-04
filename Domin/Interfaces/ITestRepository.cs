using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domin.DTO;
using Domin.Models;

namespace Domin.Interfaces
{
    public interface ITestRepository
    {
        Task<IEnumerable<TestModel>> GetTestList();
        Task<IEnumerable<TestModel>> GetTestFilterList(string? testTitle, string? testCode, DateTime? fromDate, DateTime? toDate, string IsFinish);
        Task<IEnumerable<TestModel>> GetDeletedTestList();
        Task<IEnumerable<TestModel>> GetDeletedTestFilterList(string? testTitle, string? testCode, DateTime? fromDate, DateTime? toDate, string IsFinish);
        Task<IEnumerable<TestModel>> GetFilterBoughtTests(int studentId, string search, bool IsFinished);
        Task<TestModel> GetTestById(int testId);
        Task<TestModel> GetTestByIds(int testId,int teacherId);
        Task<TestModel> GetDeletedTestById(int testId);
        Task<IEnumerable<GradeItemDTO>> GetGradeItems();
        Task<IEnumerable<LessonModel>> GetLessonItems(int gradeId);
        Task<IEnumerable<LessonModel>> GetLessonItems(int gradeId,int lessonId);
        Task<LessonModel> GetLessonId(int id);
        Task<GradeModel> GetGradeId(int id);
        Task<IEnumerable<int>> GetLessonItemsByTeacherId(int id);
        Task<IEnumerable<TeacherModel>> GetTeacherList(int lessonId);
        Task<int> GetGradeIdByLessonId(int id);
        void CreateTest(TestModel test);
        void EditTest(TestModel test);
        void RemoveTest(TestModel test);
        int GetQuestionCountsById(int testId);
        int GetAllQuestionCountsById(int testId);
        int GetDescriptiveQuestionCountsById(int testId);
        Task<List<QuestionModel>> GetQuestionsByTestId(int testId);
        Task<int> GetTestsCount();
        #region ChartInfos

        Task<int> GetActiveTestsCount(DateTime date, DateTime dtNow);
        Task<int> GetParticipatedTest(DateTime date, DateTime dtNow);
        Task<int> GetNotParticipatedTest(DateTime date, DateTime dtNow);
        Task<int> GetFinishedStudents(DateTime date, DateTime dtNow);
        Task<int> GetUnFinishedStudents(DateTime date, DateTime dtNow);
        Task<int> GetParticipatedSingleTest(DateTime date, DateTime dtNow,int testId);
        Task<int> GetNotParticipatedSingleTest(DateTime date, DateTime dtNow,int testId);
        Task<int> GetSingleFinishedStudents(DateTime date, DateTime dtNow,int testId);
        Task<int> GetSingleUnFinishedStudents(DateTime date, DateTime dtNow,int testId);
        Task<int> GetActiveTestsCount(DateTime date, DateTime dtNow, int masterId);
        Task<int> GetParticipatedTest(DateTime date, DateTime dtNow, int masterId);
        Task<int> GetNotParticipatedTest(DateTime date, DateTime dtNow, int masterId);
        Task<int> GetFinishedStudents(DateTime date, DateTime dtNow, int masterId);
        Task<int> GetUnFinishedStudents(DateTime date, DateTime dtNow, int masterId);

        #endregion

        #region TestCounts

        Task<int> GetTestCountsByGradeId(int gradeId);
        Task<int> GetTestCountsByLessonId(int lessonId);
        Task<int> GetTestCountsByTeacherId(int teacherId); 


        #endregion
        void Save();
        /************/
        Task<IEnumerable<TestModel>> GetMasterExams(int masterId);
        Task<IEnumerable<TestModel>> GetMasterExamsByFilter(int masterId,string? testTitle, string? testCode, DateTime? fromDate, DateTime? toDate, string IsFinish);
        Task<IEnumerable<TestModel>> GetDeletedMasterExams(int masterId);
        Task<IEnumerable<TestModel>> GetDeletedMasterExamsByFilter(int masterId, string? testTitle, string? testCode, DateTime? fromDate, DateTime? toDate, string IsFinish);
        Task<IEnumerable<StudentModel>> GetTestStudents(int lessonId);
        Task<IEnumerable<TestStudentsModel>> GetTestStudentById(int testId);
        Task<IEnumerable<TestStudentsModel>> GetTestStudentById();
        void AddTestStudents(TestStudentsModel model);
        void RemoveTestStudent(TestStudentsModel model);
        Task<bool> IsStartedExam(int testId, DateTime date,DateTime dtNow);
        Task<bool> IsComprehensiveTest(int testId);
        Task<bool> IsFinishedExam(int testId,DateTime date,DateTime dtNow);

        #region StudentPanel

        Task<IEnumerable<TestModel>> GetFilterTestsByLesson(int studentId,int lessonId,string search,bool IsFinished);

        #endregion
        #region Participants

        Task<IEnumerable<TestStudentsModel>> GetParticipants(int testId);

        #endregion
        #region TestStudents

        Task<TestStudentsModel> GetTestStudentByIds(int studentId,int testId);
        void UpdateTestStudents(TestStudentsModel model);

        #endregion
        Task<List<TestStudentsModel>> GetParticipants(string search);
        Task<List<TestStudentsModel>> GetParticipantsByStudentId(string search,int studentId);
        #region StudentDashboard

        Task<IEnumerable<TestModel>> GetActiveStudentTestList(DateTime date, DateTime nowDate, int studentId);
        Task<int> ActiveStudentTestsCount(DateTime date, DateTime nowDate, int studentId);

        #endregion
    }
}