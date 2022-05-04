using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domin.Interfaces;
using Domin.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    public class QuestionRepository:IQuestionRepository
    {
        private readonly ExamContext _context;

        public QuestionRepository(ExamContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<QuestionModel>> GetAllQuestion(int testId)
        {
            return await _context.QuestionModels.Where(w=>w.TestId==testId).ToListAsync();
        }
        public async Task<IEnumerable<QuestionModel>> GetTestQuestions(int testId)
        {
            return await _context.QuestionModels.Where(w => w.TestId == testId && w.Descriptive == false).ToListAsync();
        }
        public async Task<IEnumerable<QuestionModel>> GetAllDescriptiveQuestion(int testId)
        {
            return await _context.QuestionModels.Where(q => q.TestId == testId && q.Descriptive == true)
                .ToListAsync();
        }
        public async Task<QuestionModel> GetQuestionById(int id)
        {
            return await _context.QuestionModels.SingleOrDefaultAsync(s => s.Id == id);
        }

        public async Task<QuestionModel> GetQuestionByNumber(int answerNumber,int testId,int lessonId)
        {
            return await _context.QuestionModels.SingleOrDefaultAsync(q=>q.QuestionNumber == answerNumber && q.TestId == testId && q.LessonId == lessonId);
        }
        public void Create(QuestionModel model)
        {
            _context.QuestionModels.Add(model);
            Save();
        }

        public void Edit(QuestionModel model)
        {
            _context.Update(model);
            Save();
        }

        public void Delete(QuestionModel model)
        {
            _context.QuestionModels.Remove(model);Save();
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public int GetQuestionCountsByLessonId(int lessonId,int testId)
        {
            return _context.QuestionModels.Count(n => n.LessonId == lessonId && n.TestId == testId);
        }
        public async Task<IEnumerable<QuestionModel>> GetTestQuestionsByLessonId(int testId, int lessonId)
        {
            return await _context.QuestionModels.Where(n => n.TestId == testId && n.LessonId == lessonId && n.Descriptive == false).ToListAsync();
        }
        public async Task<QuestionModel> GetQuestionScore(int lessonId)
        {
            return await _context.QuestionModels.FirstOrDefaultAsync(q => q.LessonId == lessonId);
        }
    }
}
