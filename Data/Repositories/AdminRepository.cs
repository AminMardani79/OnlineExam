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
    public class AdminRepository:IAdminRepository
    {
        private readonly ExamContext _context;
         
        public AdminRepository(ExamContext context)
        {
            _context = context;
        }

        public void CreateAdmin(AdminModel admin)
        {
            _context.Add(admin);
            Save();
        }

        public void DeleteAdmin(AdminModel admin)
        {
            _context.Remove(admin);
            Save();
        }

        public async Task<AdminModel> GetAdminById(int adminId)
        {
            return await _context.AdminModels.SingleOrDefaultAsync(n=> n.AdminId == adminId);
        }

        public async Task<IEnumerable<AdminModel>> GetAdminsList(string search="")
        {
            return await _context.AdminModels.OrderByDescending(n=> n.AdminId).Where(w=>EF.Functions.Like(w.AdminName,$"%{search}%")).ToListAsync();
        }

        public async Task<AdminModel> GetDeletedAdminById(int adminId)
        {
            return await _context.AdminModels.Where(n => n.IsAdminDeleted == true).IgnoreQueryFilters().SingleOrDefaultAsync(n=> n.AdminId == adminId);
        }

        public async Task<IEnumerable<AdminModel>> GetDeletedAdminsList(string search)
        {
            return await _context.AdminModels.Where(n => n.IsAdminDeleted == true).IgnoreQueryFilters().OrderByDescending(n => n.AdminId)
                .Where(w => EF.Functions.Like(w.AdminName, $"%{search}%")).ToListAsync();
        }

        public void UpdateAdmin(AdminModel admin)
        {
            _context.Update(admin);
            Save();
        }
        public void Save()
        {
            _context.SaveChanges();
        }

        public async Task<bool> ExistAdmin(string code)
        {
            return await _context.AdminModels.AnyAsync(a => a.NationalCode == code);
        }
        public async Task<bool> IsExistAdmin(string code, string password)
        {
            return await _context.AdminModels.AnyAsync(a=> a.NationalCode == code && a.Password == password);
        }
        public async Task<AdminModel> GetAdminByLoginInfo(string code, string password)
        {
            return await _context.AdminModels.Where(w => w.ActiveAccount == true)
                .SingleOrDefaultAsync(s => s.NationalCode == code && s.Password == password);
        }
    }
}