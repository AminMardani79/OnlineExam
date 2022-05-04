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
    public class LessonRepository:ILessonRepository
    {
        private readonly ExamContext _context;

        public LessonRepository(ExamContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<LessonModel>> GetLessonList()
        {
            return await _context.LessonModels.Include(n=> n.Grade).ToListAsync();
        }
        public async Task<IEnumerable<LessonModel>> GetDeletedLessonList()
        {
            return await _context.LessonModels.Include(n=> n.Grade).Where(w => w.IsLessonDelete == true).IgnoreQueryFilters().ToListAsync();
        }

        public async Task<IEnumerable<LessonModel>> GetLessonListByFilter(string search)
        {
            return await _context.LessonModels.Include(n => n.Grade).Where(n=> EF.Functions.Like(n.LessonName,$"%{search}%")
            || EF.Functions.Like(n.Grade.GradeName,$"%{search}%")).ToListAsync();
        }

        public async Task<IEnumerable<LessonModel>> GetDeletedLessonListByFilter(string search)
        {
            return await _context.LessonModels.Include(n => n.Grade).Where(w => w.IsLessonDelete == true && (EF.Functions.Like(w.LessonName, $"{search}") ||
                EF.Functions.Like(w.Grade.GradeName, $"%{search}%"))).IgnoreQueryFilters().ToListAsync();
        }
        public async Task<LessonModel> GetLessonId(int lessonId)
        {
            return await _context.LessonModels.Include(n=> n.Grade).SingleOrDefaultAsync(s => s.LessonId == lessonId);
        }

        public async Task<LessonModel> GetDeletedLessonId(int lessonId)
        {
            return await _context.LessonModels.Where(w => w.IsLessonDelete == true).IgnoreQueryFilters().SingleOrDefaultAsync(s => s.LessonId == lessonId);

        }

        public void CreateLesson(LessonModel model)
        {
            _context.LessonModels.Add(model);Save();
        }

        public void UpdateLesson(LessonModel model)
        {
            _context.Update(model);Save();
        }

        public void DeleteLesson(LessonModel model)
        {
            _context.LessonModels.Remove(model);Save();
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public async Task<IEnumerable<LessonModel>> GetLessonsByGradeId(int gradeId)
        {
            return await _context.LessonModels.Where(n => n.GradeId == gradeId).ToListAsync();
        }

        public async Task<LessonModel> GetLessonName(int lessonId)
        {
            return await _context.LessonModels.SingleOrDefaultAsync(l => l.LessonId == lessonId);
        }
        public async Task<int> GetLessonIdByLessonName(string lessonName)
        {
            var lesson = await _context.LessonModels.SingleOrDefaultAsync(n=> n.LessonName == lessonName);
            return lesson.LessonId;
        }
    }
}