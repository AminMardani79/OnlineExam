using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domin.Interfaces;
using Domin.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    public class AnswerRepository : IAnswerRepository
    {
        private readonly ExamContext _context;

        public AnswerRepository(ExamContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<AnswerModel>> GetAnswersForStudent(int studentId, int testId)
        {
            return await _context.AnswerModels.Where(n => n.StudentId == studentId && n.TestId == testId && n.IsDescriptive == false).ToListAsync();
        }

        public async Task<IEnumerable<AnswerModel>> GetDescriptiveAnswersForStudent(int studentId, int testId)
        {
            return await _context.AnswerModels.Where(n => n.StudentId == studentId && n.TestId == testId && n.IsDescriptive == true).ToListAsync();
        }

        public string GetAnswerFile(int studentId, int testId)
        {
            return _context.AnswerModels.Where(n => n.StudentId == studentId && n.TestId == testId)
                .OrderByDescending(n => n.AnswerId).Select(n => n.AnswerFile)
                .FirstOrDefault();
        }
        public async Task<bool> IsSubmitAnswer(int studentId, int testId)
        {
            return await _context.AnswerModels.AnyAsync(n => n.StudentId == studentId && n.TestId == testId);
        }
        public void CreateAnswer(AnswerModel answer)
        {
            _context.AnswerModels.Add(answer);
            Save();
        }
        public async Task<bool> IsExistAnswer(int questionNumber)
        {
            return await _context.AnswerModels.AnyAsync(n=> n.AnswerNumber == questionNumber && n.AnswerChecked != null);
        }
        public void Save()
        {
            _context.SaveChanges();
        }
    }
}