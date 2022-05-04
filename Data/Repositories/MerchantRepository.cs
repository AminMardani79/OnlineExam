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
    public class MerchantRepository:IMerchantRepository
    {
        private readonly ExamContext _context;
        public MerchantRepository(ExamContext context)
        {
            _context = context;
        }
        public void UpdateMerchant(MerchantModel merchant)
        {
            _context.Update(merchant);
            Save();
        }
        public async Task<MerchantModel> GetFirstMerchant()
        {
            return await _context.MerchantModels.FirstOrDefaultAsync();
        }
        public void Save()
        {
            _context.SaveChanges();
        }
    }
}