using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.ViewModels.AdminViewModel;
using Application.ViewModels.TeacherViewModel;

namespace Application.Interfaces
{
    public interface IAdminService
    {
        Tuple<List<AdminViewModel>,int,int> GetAdmins(string search = "", int page = 1);
        Task<EditAdminViewModel> GetAdminById(int id);
        void Create(AddAdminViewModel model);
        void Update(EditAdminViewModel model);
        Tuple<List<AdminViewModel>, int, int> GetTrashAdmins(string search = "", int page = 1);
        void Delete(int id); void Back(int id); void Remove(int id);
        Task<bool> ExistAdmin(string code);
        Task<bool> IsExistAdmin(RequestAdminLoginViewModel model);
        Task<AdminLoginViewModel> GetAdminLoginInfo(RequestAdminLoginViewModel model);
        Task<AdminProfileViewModel> GetAdminProfile(int adminId);
    }
}
