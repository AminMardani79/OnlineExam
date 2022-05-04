using Application.Interfaces;
using Domin.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Others;
using Application.ViewModels.AdminViewModel;
using Application.ViewModels.StudentViewModel;
using Domin.Models;
using Application.ViewModels.TeacherViewModel;

namespace Application.Services
{
    public class AdminService:IAdminService
    {
        private readonly IAdminRepository _adminRepository;
        public AdminService(IAdminRepository adminRepository)
        {
            _adminRepository = adminRepository;
        }

        public  Tuple<List<AdminViewModel>, int, int> GetAdmins(string search = "", int page = 1)
        {
            var list = _adminRepository.GetAdminsList(search).Result;
            List<AdminViewModel> models = new List<AdminViewModel>();
            int pageNumber = page;
            int pageCount = PageCounts.PageCount(list.Count(), 6);
            int skip = (page - 1) * 6;
            var adminList = list.Skip(skip).Take(6).ToList();
            foreach (var item in adminList)
            {
               models.Add(new AdminViewModel()
               {
                   AdminEmail = item.AdminEmail,
                   AdminId = item.AdminId,
                   AdminImage = item.AdminImage,
                   AdminName = item.AdminName,
                   NationalCode = item.NationalCode,
                   PhoneNumber = item.PhoneNumber ,
                   RoleId = item.RoleId
               });
            }
            return Tuple.Create(models, pageCount, pageNumber);
        }

        public async Task<EditAdminViewModel> GetAdminById(int id)
        {
            var item =await _adminRepository.GetAdminById(id);
            EditAdminViewModel edit=new EditAdminViewModel();
            edit.About = item.About;
            edit.ActiveAccount = item.ActiveAccount;
            edit.AdminEmail = item.AdminEmail;
            edit.AdminId = item.AdminId;
            edit.NationalCode = item.NationalCode;
            edit.OldCode = item.NationalCode;
            edit.AdminEmail = item.AdminEmail;
            edit.PhoneNumber = item.PhoneNumber;
            edit.OldPassword = item.Password;
            edit.AdminImage = item.AdminImage;
            edit.AdminName = item.AdminName;
            return edit;
        }

        public void Create(AddAdminViewModel model)
        {
           AdminModel admin=new AdminModel();
           admin.Password = HashPassword.Coding(model.Password);
           admin.About = model.About;
           admin.ActiveAccount = model.ActiveAccount;
           admin.AdminEmail = model.AdminEmail;
           admin.AdminName = model.AdminName;
           admin.NationalCode = model.NationalCode;
           admin.PhoneNumber = model.PhoneNumber;
           if (model.Avatar != null)
           {
               var check = model.Avatar.IsImage();
               if (check)
               {
                   admin.AdminImage = ImageConvertor.SaveImage(model.Avatar);
               }
               else
               {
                   admin.AdminImage = "masterAvatar.png";
               }
           }
           else
           {
               admin.AdminImage = "masterAvatar.png";
           }

            admin.RoleId = 1;
           _adminRepository.CreateAdmin(admin);
        }

        public void Update(EditAdminViewModel model)
        {
            var edit = _adminRepository.GetAdminById(model.AdminId).Result;
            if (model.Password != null)
            {
                edit.Password = HashPassword.Coding(model.Password);
            }
            else
            {
                edit.Password = model.OldPassword;
            }
            edit.About = model.About;
            edit.ActiveAccount = model.ActiveAccount;
            edit.AdminEmail = model.AdminEmail;
            edit.AdminName = model.AdminName;
            edit.NationalCode = model.NationalCode;
            edit.PhoneNumber = model.PhoneNumber;
            if (model.Avatar != null)
            {
                var check = model.Avatar.IsImage();
                if (check)
                {
                    edit.AdminImage = ImageConvertor.SaveImage(model.Avatar);
                }
            }
            _adminRepository.UpdateAdmin(edit);
        }

        public Tuple<List<AdminViewModel>, int, int> GetTrashAdmins(string search = "", int page = 1)
        {
            var list = _adminRepository.GetDeletedAdminsList(search).Result;
            List<AdminViewModel> models = new List<AdminViewModel>();
            int pageNumber = page;
            int pageCount = PageCounts.PageCount(list.Count(), 6);
            int skip = (page - 1) * 6;
            var adminList = list.Skip(skip).Take(6).ToList();
            foreach (var item in adminList)
            {
                models.Add(new AdminViewModel()
                {
                    AdminEmail = item.AdminEmail,
                    AdminId = item.AdminId,
                    AdminImage = item.AdminImage,
                    AdminName = item.AdminName,
                    NationalCode = item.NationalCode,
                    PhoneNumber = item.PhoneNumber,
                    RoleId = item.RoleId
                });
            }
            return Tuple.Create(models, pageCount, pageNumber);
        }

        public void Delete(int id)
        {
            var model = _adminRepository.GetAdminById(id).Result;
            model.IsAdminDeleted = true;
            _adminRepository.UpdateAdmin(model);
        }

        public void Back(int id)
        {
            var model = _adminRepository.GetDeletedAdminById(id).Result;
            model.IsAdminDeleted = false;
            _adminRepository.UpdateAdmin(model);
        }

        public void Remove(int id)
        {
            var model = _adminRepository.GetDeletedAdminById(id).Result;
            ImageConvertor.RemoveImage(model.AdminImage);
           _adminRepository.DeleteAdmin(model);
        }

        public async Task<bool> ExistAdmin(string code)
        {
            var result =await _adminRepository.ExistAdmin(code);
            return result;
        }
        public async Task<bool> IsExistAdmin(RequestAdminLoginViewModel model)
        {
            var hash = HashPassword.Coding(model.AdminPassword);
            var result = await _adminRepository.IsExistAdmin(model.AdminCode,hash);
            return result;
        }
        public async Task<AdminLoginViewModel> GetAdminLoginInfo(RequestAdminLoginViewModel model)
        {
            var hash = HashPassword.Coding(model.AdminPassword);
            var result = await _adminRepository.GetAdminByLoginInfo(model.AdminCode, hash);
            if (result != null)
            {
                AdminLoginViewModel login = new AdminLoginViewModel();
                login.AdminImage = result.AdminImage;
                login.AdminName = result.AdminName;
                login.AdminId = result.AdminId;
                login.AdminEmail = result.AdminEmail;
                login.RoleId = result.RoleId;
                login.AdminNationalCode = result.NationalCode;
                return login;
            }
            else
            {
                return null;
            }
        }
        public async Task<AdminProfileViewModel> GetAdminProfile(int adminId)
        {
            var admin = await _adminRepository.GetAdminById(adminId);
            AdminProfileViewModel model = new AdminProfileViewModel();
            model.About = admin.About;
            model.AdminEmail = admin.AdminEmail;
            model.AdminName = admin.AdminName;
            model.Avatar = admin.AdminImage;
            model.NationalCode = admin.NationalCode;
            model.PhoneNumber = admin.PhoneNumber;
            return model;
        }
    }
}