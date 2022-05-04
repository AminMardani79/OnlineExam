using Domin.Interfaces;
using Domin.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class WorkBookRepository:IWorkBookRepository
    {
        private readonly ExamContext _context;

        public WorkBookRepository(ExamContext context)
        {
            _context = context;
        }
        public async Task<WorkBookModel> GetWorkBookById(int id)
        {
            return await _context.WorkBookModels.SingleOrDefaultAsync(n=> n.WorkBookId == id);
        }
        public void CreteWorkBook(WorkBookModel model)
        {
            _context.Add(model);
            Save();
        }
        public void EditWorkBook(WorkBookModel model)
        {
            _context.Update(model);
            Save();
        }
        public async Task<bool> IsSubmitWorkBook(int studentId, int testId)
        {
            return await _context.WorkBookModels.AnyAsync(w=> w.StudentId == studentId && w.TestId == testId);
        }
        public async Task<List<WorkBookModel>> GetWorkBookByIds(int studentId, int testId)
        {
            return await _context.WorkBookModels.Where(n=> n.TestId == testId && n.StudentId == studentId).ToListAsync();
        }
        public async Task<WorkBookModel> GetHighestPercent(int testId, string lessonName)
        {
            return await _context.WorkBookModels.OrderByDescending(w => w.Percent).FirstOrDefaultAsync(w => w.TestId == testId && w.LessonName == lessonName);
        }
        public async Task<WorkBookModel> GetHighestLevel(int testId, string lessonName)
        {
            return await _context.WorkBookModels.OrderByDescending(w => w.Level).FirstOrDefaultAsync(w => w.TestId == testId && w.LessonName == lessonName);
        }
        public async Task<int> ParticipantCounts(int testId)
        {
            return await _context.TestStudentsModels.Include(n => n.TestModel).ThenInclude(n => n.AnswerModels)
                .CountAsync(n=> n.TestId == testId && n.TestModel.AnswerModels.Count() != 0);
        }
        public async Task<List<WorkBookModel>> GetWorkBookByIds(int testId, string lessonName)
        {
            return await _context.WorkBookModels.Where(n => n.TestId == testId && n.LessonName == lessonName).OrderByDescending(n=> n.Percent).ToListAsync();
        }
        public async Task<double> GetAveragePercent(int testId, string lessonName)
        {
            return await _context.WorkBookModels.Where(n => n.TestId == testId && n.LessonName == lessonName).AverageAsync(n=> n.Percent);
        }
        public async Task<List<double>> GetAllPercents(int testId, string lessonName)
        {
            return await _context.WorkBookModels.Where(n => n.TestId == testId && n.LessonName == lessonName).Select(n => n.Percent).ToListAsync();
        }
        public int GetWorkBooksCount(int testId,string lessonName)
        {
            return _context.WorkBookModels.Count(n=> n.TestId == testId && n.LessonName == lessonName);
        }
        public async Task<int> GetWorkBooksCount()
        {
            return await _context.WorkBookModels.CountAsync();
        }
        public int GetCorrectsCountById(int testId, int studentId)
        {
            return _context.WorkBookModels.Where(n => n.TestId == testId && n.StudentId == studentId).Sum(n=> n.TrueAnswers);
        }
        public int GetWrongsCountById(int testId, int studentId)
        {
            return _context.WorkBookModels.Where(n => n.TestId == testId && n.StudentId == studentId).Sum(n => n.WrongAnswers);
        }
        public int GetNoCheckedById(int testId, int studentId)
        {
            return _context.WorkBookModels.Where(n => n.TestId == testId && n.StudentId == studentId).Sum(n => n.NoCheckedAnswers);
        }
        public async Task<IEnumerable<LevelPercentModel>> GetLevelPercentsById(int studentId)
        {
            return await _context.LevelPercentModels.Where(n => n.StudentId == studentId).ToListAsync();
        }
        public async Task<LevelPercentModel> GetLevelPercentByIds(int testId, int studentId)
        {
            return await _context.LevelPercentModels.SingleOrDefaultAsync(n=> n.TestId == testId && n.StudentId == studentId);
        }
        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
