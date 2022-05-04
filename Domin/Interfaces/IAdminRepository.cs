using Domin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domin.Interfaces
{
    public interface IAdminRepository
    {
        Task<IEnumerable<AdminModel>> GetAdminsList(string search="");
        Task<IEnumerable<AdminModel>> GetDeletedAdminsList(string search = "");
        Task<AdminModel> GetAdminById(int adminId); 
        Task<AdminModel> GetDeletedAdminById(int adminId);
        void CreateAdmin(AdminModel admin);
        void UpdateAdmin(AdminModel admin);
        void DeleteAdmin(AdminModel admin);
        void Save();
        Task<bool> ExistAdmin(string code);
        Task<bool> IsExistAdmin(string code,string password);
        Task<AdminModel> GetAdminByLoginInfo(string code, string password);
    }
}