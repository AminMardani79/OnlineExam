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
    public class LevelPercentRepository:ILevelPercentRepository
    {
        private readonly ExamContext _context;

        public LevelPercentRepository(ExamContext context)
        {
            _context = context;
        }

        public void CreateLevelPercent(LevelPercentModel model)
        {
            _context.Add(model);
            Save();
        }

        public void UpdateLevelPercent(LevelPercentModel model)
        {
            _context.Update(model);
            Save();
        }
        public async Task<LevelPercentModel> GetModelById(int id)
        {
            return await _context.LevelPercentModels.SingleOrDefaultAsync(n=> n.LevelPercentId == id);
        }
        public async Task<LevelPercentModel> GetLevelPercentByIds(int studentId, int testId)
        {
            return await _context.LevelPercentModels.SingleOrDefaultAsync(n=> n.StudentId == studentId && n.TestId == testId);
        }
        public async Task<List<LevelPercentModel>> GetRanksByDecending(int testId)
        {
            return await _context.LevelPercentModels.OrderByDescending(n => n.TestScore).Where(n => n.TestId == testId).ToListAsync();
        }
        public async Task<LevelPercentModel> GetHighestAverageLevel(int testId)
        {
            return await _context.LevelPercentModels.OrderByDescending(n => n.TestLevel).FirstOrDefaultAsync(n=> n.TestId == testId);
        }
        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
