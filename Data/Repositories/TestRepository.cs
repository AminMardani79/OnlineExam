using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domin.DTO;
using Domin.Interfaces;
using Domin.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    public class TestRepository : ITestRepository
    {
        private readonly ExamContext _context;

        public TestRepository(ExamContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<TestModel>> GetTestList()
        {
            return await _context.TestModels.ToListAsync();
        }

        public async Task<IEnumerable<TestModel>> GetTestFilterList(string? testTitle, string? testCode, DateTime? fromDate,
            DateTime? toDate, string IsFinish)
        {
            if (IsFinish == "true" || IsFinish == "false")
            {
                bool IsFinished = Convert.ToBoolean(IsFinish);
                return await _context.TestModels.Where(t => EF.Functions.Like(t.TestTitle, $"%{testTitle}%")
                                                            && EF.Functions.Like(t.TestCode, $"%{testCode}%")
                                                            && t.Finish != IsFinished && (t.TestDayTime <= toDate && t.TestDayTime >= fromDate)).ToListAsync();
            }
            else
            {
                return await _context.TestModels.Where(t => EF.Functions.Like(t.TestTitle, $"%{testTitle}%")
                                                            && EF.Functions.Like(t.TestCode, $"%{testCode}%")
                                                            && (t.TestDayTime <= toDate && t.TestDayTime >= fromDate)).ToListAsync();
            }
        }
        public async Task<IEnumerable<TestModel>> GetDeletedTestList()
        {
            return await _context.TestModels.Where(n => n.IsDelete == true).IgnoreQueryFilters().ToListAsync();
        }

        public async Task<IEnumerable<TestModel>> GetDeletedTestFilterList(string? testTitle, string? testCode,
            DateTime? fromDate, DateTime? toDate, string IsFinish)
        {
            if (IsFinish == "true" || IsFinish == "false")
            {
                bool IsFinished = Convert.ToBoolean(IsFinish);
                return await _context.TestModels.Where(t => t.IsDelete == true && EF.Functions.Like(t.TestTitle, $"%{testTitle}%")
                                                            && EF.Functions.Like(t.TestCode, $"%{testCode}%")
                                                            && t.Finish != IsFinished && (t.TestDayTime <= toDate && t.TestDayTime >= fromDate)).IgnoreQueryFilters().ToListAsync();
            }
            else
            {
                return await _context.TestModels.Where(t => t.IsDelete == true && EF.Functions.Like(t.TestTitle, $"%{testTitle}%") && EF.Functions.Like(t.TestCode, $"%{testCode}%")
                                                                               && (t.TestDayTime < toDate && t.TestDayTime > fromDate)).IgnoreQueryFilters().ToListAsync();
            }
        }
        public async Task<TestModel> GetTestById(int testId)
        {
            return await _context.TestModels.SingleOrDefaultAsync(n => n.TestId == testId);
        }
        public async Task<TestModel> GetTestByIds(int testId,int teacherId)
        {
            return await _context.TestModels.SingleOrDefaultAsync(n => n.TestId == testId && n.TeacherId == teacherId);
        }
        public async Task<TestModel> GetDeletedTestById(int testId)
        {
            return await _context.TestModels.Include(n=> n.Question).Where(n => n.IsDelete == true).IgnoreQueryFilters()
                .SingleOrDefaultAsync(n => n.TestId == testId);
        }
        public async Task<IEnumerable<GradeItemDTO>> GetGradeItems()
        {
            return await _context.GradeModels.Select(s => new GradeItemDTO()
            {
                ItemGradeId = s.GradeId,
                ItemGradeName = s.GradeName
            }).ToListAsync();

        }
        public async Task<IEnumerable<LessonModel>> GetLessonItems(int gradeId)
        {
            return await _context.LessonModels.Where(n => n.GradeId == gradeId).ToListAsync();
        }
        public async Task<IEnumerable<LessonModel>> GetLessonItems(int gradeId,int lessonId)
        {
            return await _context.LessonModels.Where(n => n.GradeId == gradeId && n.LessonId != lessonId).ToListAsync();
        }
        public async Task<LessonModel> GetLessonId(int id)
        {
            return await _context.LessonModels.SingleOrDefaultAsync(s => s.LessonId == id);
        }

        public async Task<GradeModel> GetGradeId(int id)
        {
            return await _context.GradeModels.SingleOrDefaultAsync(s => s.GradeId == id);
        }

        public async Task<IEnumerable<int>> GetLessonItemsByTeacherId(int id)
        {
            return await _context.TeacherLessonModels.Where(w => w.TeacherId == id).Select(s => s.LessonId).ToListAsync();
        }

        public async Task<IEnumerable<TeacherModel>> GetTeacherList(int lessonId)
        {
            return await _context.TeacherLessonModels.Include(n => n.TeacherModel)
                .Where(n => n.LessonId == lessonId).Select(n => n.TeacherModel).ToListAsync();
        }

        public async Task<int> GetGradeIdByLessonId(int id)
        {
            return await _context.LessonModels.Where(w => w.LessonId == id).Select(s => s.GradeId).SingleOrDefaultAsync();
        }

        public void CreateTest(TestModel test)
        {
            _context.TestModels.Add(test); Save();
        }

        public void EditTest(TestModel test)
        {
            _context.TestModels.Update(test); Save();
        }

        public void RemoveTest(TestModel test)
        {
            _context.Remove(test); Save();
        }
        public async Task<int> GetTestsCount()
        {
            return await _context.TestModels.CountAsync();
        }
        #region ChartInfos

        public async Task<int> GetActiveTestsCount(DateTime date, DateTime dtNow)
        {
            return await _context.TestModels.CountAsync(n => n.StartTest <= date && n.EndTest >= date && n.TestDayTime == dtNow);
        }
        public async Task<int> GetParticipatedTest(DateTime date, DateTime dtNow)
        {
            return await _context.TestStudentsModels.Include(n => n.TestModel).CountAsync(n => n.EnterCount > 0 && n.TestModel.StartTest <= date && n.TestModel.EndTest >= date && n.TestModel.TestDayTime == dtNow);
        }
        public async Task<int> GetNotParticipatedTest(DateTime date, DateTime dtNow)
        {
            return await _context.TestStudentsModels.Include(n => n.TestModel).CountAsync(n => n.EnterCount == 0 && n.TestModel.StartTest <= date && n.TestModel.EndTest >= date && n.TestModel.TestDayTime == dtNow);
        }
        public async Task<int> GetFinishedStudents(DateTime date, DateTime dtNow)
        {
            return await _context.TestStudentsModels.CountAsync(n => n.IsSubmitAnswer == true && n.TestModel.StartTest <= date && n.TestModel.EndTest >= date && n.TestModel.TestDayTime == dtNow);
        }
        public async Task<int> GetUnFinishedStudents(DateTime date, DateTime dtNow)
        {
            return await _context.TestStudentsModels.CountAsync(n => n.IsSubmitAnswer == false && n.TestModel.StartTest <= date && n.TestModel.EndTest >= date && n.TestModel.TestDayTime == dtNow);
        }
        public async Task<int> GetParticipatedSingleTest(DateTime date, DateTime dtNow, int testId)
        {
            return await _context.TestStudentsModels.CountAsync(n => n.EnterCount > 0 && n.TestModel.StartTest <= date && n.TestModel.EndTest >= date && n.TestModel.TestDayTime == dtNow && n.TestId == testId);
        }
        public async Task<int> GetNotParticipatedSingleTest(DateTime date, DateTime dtNow, int testId)
        {
            return await _context.TestStudentsModels.CountAsync(n => n.EnterCount == 0 && n.TestModel.StartTest <= date && n.TestModel.EndTest >= date && n.TestModel.TestDayTime == dtNow && n.TestId == testId);
        }
        public async Task<int> GetSingleFinishedStudents(DateTime date, DateTime dtNow, int testId)
        {
            return await _context.TestStudentsModels.CountAsync(n => n.IsSubmitAnswer == true && n.TestModel.StartTest <= date && n.TestModel.EndTest >= date && n.TestModel.TestDayTime == dtNow && n.TestId == testId);
        }
        public async Task<int> GetSingleUnFinishedStudents(DateTime date, DateTime dtNow, int testId)
        {
            return await _context.TestStudentsModels.CountAsync(n => n.IsSubmitAnswer == false && n.TestModel.StartTest <= date && n.TestModel.EndTest >= date && n.TestModel.TestDayTime == dtNow && n.TestId == testId);
        }
        public async Task<int> GetActiveTestsCount(DateTime date, DateTime dtNow, int masterId)
        {
            return await _context.TestModels.CountAsync(n => n.StartTest <= date && n.EndTest >= date && n.TestDayTime == dtNow && n.TeacherId == masterId);
        }
        public async Task<int> GetParticipatedTest(DateTime date, DateTime dtNow, int masterId)
        {
            return await _context.TestStudentsModels.Include(n => n.TestModel).CountAsync(n => n.EnterCount > 0 && n.TestModel.StartTest <= date && n.TestModel.EndTest >= date && n.TestModel.TestDayTime == dtNow && n.TestModel.TeacherId == masterId);
        }
        public async Task<int> GetNotParticipatedTest(DateTime date, DateTime dtNow, int masterId)
        {
            return await _context.TestStudentsModels.Include(n => n.TestModel).CountAsync(n => n.EnterCount == 0 && n.TestModel.StartTest <= date && n.TestModel.EndTest >= date && n.TestModel.TestDayTime == dtNow && n.TestModel.TeacherId == masterId);
        }
        public async Task<int> GetFinishedStudents(DateTime date, DateTime dtNow, int masterId)
        {
            return await _context.TestStudentsModels.Include(n=> n.TestModel).CountAsync(n => n.IsSubmitAnswer == true && n.TestModel.StartTest <= date && n.TestModel.EndTest >= date && n.TestModel.TestDayTime == dtNow && n.TestModel.TeacherId == masterId);
        }
        public async Task<int> GetUnFinishedStudents(DateTime date, DateTime dtNow, int masterId)
        {
            return await _context.TestStudentsModels.Include(n=> n.TestModel).CountAsync(n => n.IsSubmitAnswer == false && n.TestModel.StartTest <= date && n.TestModel.EndTest >= date && n.TestModel.TestDayTime == dtNow && n.TestModel.TeacherId == masterId);
        }

        #endregion

        #region TestCounts

        public async Task<int> GetTestCountsByGradeId(int gradeId)
        {
            return await _context.TestModels.Include(n => n.Grade).Where(n => n.Grade.IsGradeDelete == true && n.GradeId == gradeId).IgnoreQueryFilters().CountAsync();
        }
        public async Task<int> GetTestCountsByLessonId(int lessonId)
        {
            return await _context.TestModels.Include(n => n.Lesson).Where(n => n.Lesson.IsLessonDelete == true && n.LessonId == lessonId).IgnoreQueryFilters().CountAsync();
        }
        public async Task<int> GetTestCountsByTeacherId(int teacherId)
        {
            return await _context.TestModels.Include(n => n.Teacher).Where(n => n.Teacher.IsTeacherDeleted == true && n.TeacherId == teacherId).IgnoreQueryFilters().CountAsync();
        }

        #endregion
        public void Save()
        {
            _context.SaveChanges();
        }

        public int GetQuestionCountsById(int testId)
        {
            return _context.QuestionModels.Count(q => q.TestId == testId && q.Descriptive == false);
        }

        public int GetAllQuestionCountsById(int testId)
        {
            return _context.QuestionModels.Count(q => q.TestId == testId);
        }
        public int GetDescriptiveQuestionCountsById(int testId)
        {
            return _context.QuestionModels.Count(q => q.TestId == testId && q.Descriptive == true);
        }
        public async Task<List<QuestionModel>> GetQuestionsByTestId(int testId)
        {
            return await _context.QuestionModels.Where(q => q.TestId == testId && q.Descriptive == false).ToListAsync();
        }
        public async Task<IEnumerable<TestModel>> GetMasterExams(int masterId)
        {
            return await _context.TestModels.Where(w => w.TeacherId == masterId).ToListAsync();
        }

        public async Task<IEnumerable<TestModel>> GetMasterExamsByFilter(int masterId, string? testTitle,
            string? testCode, DateTime? fromDate, DateTime? toDate, string IsFinish)
        {
            if (IsFinish == "true" || IsFinish == "false")
            {
                bool IsFinished = Convert.ToBoolean(IsFinish);
                return await _context.TestModels.Where(t => t.TeacherId == masterId && EF.Functions.Like(t.TestTitle, $"%{testTitle}%")
                                                            && EF.Functions.Like(t.TestCode, $"%{testCode}%")
                                                            && t.Finish != IsFinished && (t.TestDayTime <= toDate && t.TestDayTime >= fromDate)).ToListAsync();
            }
            else
            {
                return await _context.TestModels.Where(t => t.TeacherId == masterId && EF.Functions.Like(t.TestTitle, $"%{testTitle}%")
                    && EF.Functions.Like(t.TestCode, $"%{testCode}%")
                                                            && (t.TestDayTime <= toDate && t.TestDayTime >= fromDate)).ToListAsync();
            }
        }

        public async Task<IEnumerable<TestModel>> GetDeletedMasterExams(int masterId)
        {
            return await _context.TestModels.Where(w => w.TeacherId == masterId && w.IsDelete == true)
                .IgnoreQueryFilters().ToListAsync();
        }

        public async Task<IEnumerable<TestModel>> GetDeletedMasterExamsByFilter(int masterId, string? testTitle,
            string? testCode, DateTime? fromDate, DateTime? toDate, string IsFinish)
        {
            if (IsFinish == "true" || IsFinish == "false")
            {
                bool IsFinished = Convert.ToBoolean(IsFinish);
                return await _context.TestModels.Where(t => t.TeacherId == masterId && EF.Functions.Like(t.TestTitle, $"%{testTitle}%")
                    && EF.Functions.Like(t.TestCode, $"%{testCode}%")
                    && t.Finish != IsFinished && (t.TestDayTime <= toDate && t.TestDayTime >= fromDate) && t.IsDelete == true).IgnoreQueryFilters().ToListAsync();
            }
            else
            {
                return await _context.TestModels.Where(t => t.TeacherId == masterId && EF.Functions.Like(t.TestTitle, $"%{testTitle}%")
                    && EF.Functions.Like(t.TestCode, $"%{testCode}%")
                    && (t.TestDayTime <= toDate && t.TestDayTime >= fromDate) && t.IsDelete == true).IgnoreQueryFilters().ToListAsync();
            }
        }
        public async Task<IEnumerable<StudentModel>> GetTestStudents(int gradeId)
        {
            return await _context.StudentModels.Where(n => n.GradeId == gradeId).ToListAsync();
        }

        public async Task<IEnumerable<TestStudentsModel>> GetTestStudentById(int testId)
        {
            return await _context.TestStudentsModels.Where(t => t.TestId == testId).ToListAsync();
        }
        public async Task<IEnumerable<TestStudentsModel>> GetTestStudentById()
        {
            return await _context.TestStudentsModels.ToListAsync();
        }
        public void AddTestStudents(TestStudentsModel model)
        {
            _context.Add(model);
            Save();
        }

        public void RemoveTestStudent(TestStudentsModel model)
        {
            _context.Remove(model);
            Save();
        }

        public async Task<bool> IsStartedExam(int testId, DateTime date, DateTime dtNow)
        {
            return await _context.TestModels.AnyAsync(t => t.TestId == testId && t.StartTest <= date &&
                                                                              t.EndTest >= date && t.TestDayTime == dtNow);
        }
        public async Task<bool> IsComprehensiveTest(int testId)
        {
            return await _context.TestModels.AnyAsync(t => t.TestId == testId && t.IsComprehensiveTest == true);
        }
        public async Task<bool> IsFinishedExam(int testId,DateTime date, DateTime dtNow)
        {
            return await _context.TestModels.AnyAsync(n=> n.TestId == testId && (n.EndTest <= date && n.TestDayTime <= dtNow));
        }
        #region StudentPanel

        public async Task<IEnumerable<TestModel>> GetFilterTestsByLesson(int studentId, int lessonId, string search, bool IsFinished)
        {
            if (IsFinished == true)
            {
                return await _context.TestStudentsModels.Include(n => n.TestModel)
                    .Where(n => n.StudentId == studentId && n.TestModel.LessonId == lessonId &&
                                EF.Functions.Like(n.TestModel.TestTitle, $"%{search}%") && n.TestModel.Finish != IsFinished)
                    .Select(n => n.TestModel).ToListAsync();
            }
            else
            {
                return await _context.TestStudentsModels.Include(n => n.TestModel)
                    .Where(n => n.StudentId == studentId && n.TestModel.LessonId == lessonId &&
                                EF.Functions.Like(n.TestModel.TestTitle, $"%{search}%"))
                    .Select(n => n.TestModel).ToListAsync();
            }
        }
        public async Task<IEnumerable<TestModel>> GetFilterBoughtTests(int studentId, string search, bool IsFinished)
        {
            if (IsFinished == true)
            {
                return await _context.OrderDetailModels.Include(n => n.TestModel).Include(n=> n.Order)
                    .Where(n => n.Order.StudentId == studentId &&
                                EF.Functions.Like(n.TestModel.TestTitle, $"%{search}%") && n.TestModel.Finish != IsFinished && n.Order.IsFinally)
                    .Select(n => n.TestModel).ToListAsync();
            }
            else
            {
                return await _context.OrderDetailModels.Include(n => n.TestModel).Include(n=> n.Order)
                    .Where(n => n.Order.StudentId == studentId && n.Order.IsFinally &&
                                EF.Functions.Like(n.TestModel.TestTitle, $"%{search}%"))
                    .Select(n => n.TestModel).ToListAsync();
            }
        }
        #endregion

        #region Participants

        public async Task<IEnumerable<TestStudentsModel>> GetParticipants(int testId)
        {
            return await _context.TestStudentsModels.Where(n => n.TestId == testId).ToListAsync();
        }

        #endregion
        
        #region TestStudents

        public async Task<TestStudentsModel> GetTestStudentByIds(int studentId, int testId)
        {
            return await _context.TestStudentsModels.SingleOrDefaultAsync(n=> n.StudentId == studentId && n.TestId == testId);
        }
        public void UpdateTestStudents(TestStudentsModel model)
        {
            _context.Update(model);
            Save();
        }
        #endregion
        #region ShowWorkBooksList

        public async Task<List<TestStudentsModel>> GetParticipants(string search)
        {
            return await _context.TestStudentsModels.Include(n=> n.TestModel).Where(n=> EF.Functions.Like(n.TestModel.TestTitle,$"%{search}%")).ToListAsync();
        }
        public async Task<List<TestStudentsModel>> GetParticipantsByStudentId(string search, int studentId)
        {
            return await _context.TestStudentsModels.Include(n => n.TestModel).ThenInclude(n=> n.Lesson)
                .Where(n => EF.Functions.Like(n.TestModel.Lesson.LessonName, $"%{search}%") && n.StudentId == studentId && n.IsShowingWorkBook == true).ToListAsync();
        }

        #endregion
        #region StudentDashboard

        public async Task<IEnumerable<TestModel>> GetActiveStudentTestList(DateTime date, DateTime nowDate, int studentId)
        {
            return await _context.TestStudentsModels.Include(n => n.TestModel).Where(n => n.StudentId == studentId && n.TestModel.StartTest <= date
             && n.TestModel.EndTest >= date && n.TestModel.TestDayTime == nowDate).Select(n => n.TestModel).ToListAsync();
        }
        public async Task<int> ActiveStudentTestsCount(DateTime date, DateTime nowDate, int studentId)
        {
            return await _context.TestStudentsModels.Include(n => n.TestModel).Where(n => n.StudentId == studentId && n.TestModel.StartTest <= date
             && n.TestModel.EndTest >= date && n.TestModel.TestDayTime == nowDate).Select(n => n.TestModel).CountAsync();
        }

        #endregion
    }
}