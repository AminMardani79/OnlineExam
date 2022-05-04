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
    public class DashboardRepository:IDashboardRepository
    {
        private readonly ExamContext _context;
        public DashboardRepository(ExamContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<TestModel>> GetActiveTestList(DateTime date, DateTime dtNow)
        {
            return await _context.TestModels.Where(t => t.StartTest <= date && t.EndTest >= date && t.Finish == false && t.TestDayTime == dtNow).ToListAsync();
        }
        public async Task<IEnumerable<TestModel>> GetActiveTestList(DateTime date, DateTime dtNow, int masterId)
        {
            return await _context.TestModels.Where(t => t.StartTest <= date && t.EndTest >= date && t.Finish == false && t.TestDayTime == dtNow && t.TeacherId == masterId).ToListAsync();
        }
    }
}