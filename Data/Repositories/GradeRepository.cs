using Domin.Interfaces;
using Domin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    public class GradeRepository:IGradeRepository
    {
        private readonly ExamContext _context;
        public GradeRepository(ExamContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<GradeModel>> GetAllGrades()
        {
            return await _context.GradeModels.ToListAsync();
        }
        public async Task<IEnumerable<GradeModel>> GetAllDeletedGrades()
        {
            return await _context.GradeModels.Where(n => n.IsGradeDelete==true).IgnoreQueryFilters().ToListAsync();
        }
        public async Task<GradeModel> GetGradeById(int gradeId)
        {
            return await _context.GradeModels.SingleOrDefaultAsync(s => s.GradeId == gradeId);
        }

        public async Task<GradeModel> GetDeletedGradeById(int gradeId)
        {
            return await _context.GradeModels.Include(n=> n.Lessons).Where(w=>w.IsGradeDelete==true).IgnoreQueryFilters().SingleOrDefaultAsync(s => s.GradeId == gradeId);
        }

        public void AddGrade(GradeModel grade)
        {
            _context.GradeModels.Add(grade);
            Save();
        }

        public void DeleteGrade(GradeModel model)
        {
            //If the method is not async, you must add the **"Result"** code to the end
            _context.Remove(model); Save();
        }

        public void UpdateGrade(GradeModel grade)
        {
            _context.Update(grade); Save();
        }
        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
