using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Interfaces;
using Application.ViewModels.AdminViewModel;
using PonishaExam.Helper;

namespace PonishaExam.Areas.Admin.Controllers
{
    [Area("Admin")]
    [AdminAuthorize]
    public class AdminController : Controller
    {
        private readonly IAdminService _admin;

        public AdminController(IAdminService admin)
        {
            _admin = admin;
        }

        [HttpGet]
        [Route("/AdminPanel/AdminList/{search?}")]
        public IActionResult Index(string search = "", int page = 1)
        {
            var model = _admin.GetAdmins(search ?? "", page);
            ViewBag.search = search;
            return View(model);
        }

        [HttpGet]
        [Route("/AdminPanel/CreateAdmin")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Route("/AdminPanel/CreateAdmin")]
        public IActionResult Create(AddAdminViewModel model)
        {
            if (ModelState.IsValid)
            {
                var exist = _admin.ExistAdmin(model.NationalCode).Result;
                if (exist)
                {
                    ViewBag.Error = "مدیری با این کد ملی وجود دارد";
                    return View(model);
                }
                else
                {
                    _admin.Create(model);
                    return RedirectToAction(nameof(Index));
                }
            }
            else
            {
                return View(model);
            }
        }

        [HttpGet]
        [Route("/AdminPanel/EditAdmin/{id}")]
        public IActionResult Edit(int id)
        {
            var model = _admin.GetAdminById(id).Result;
            return View(model);
        }

        [HttpPost]
        [Route("/AdminPanel/EditAdmin/{id}")]
        public IActionResult Edit(EditAdminViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.OldCode != model.NationalCode)
                {
                    var exist = _admin.ExistAdmin(model.NationalCode).Result;
                    if (exist)
                    {
                        ViewBag.Error = "مدیری با این کد ملی وجود دارد";
                        return View(model);
                    }
                }
                _admin.Update(model);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(model);
            }
        }

        [HttpGet]
        [Route("/AdminPanel/DeleteAdmin/{id}")]
        public void Delete(int id)
        {
            _admin.Delete(id);
        }

        [HttpGet]
        [Route("/AdminPanel/AdminTrashList/{search?}")]
        public IActionResult Trash(string search = "", int page = 1)
        {
            var model = _admin.GetTrashAdmins(search ?? "", page);
            ViewBag.search = search;
            return View(model);
        }
        [HttpGet]
        [Route("/AdminPanel/BackAdmin/{id}")]
        public void Back(int id)
        {
            _admin.Back(id);
        }
        [HttpGet]
        [Route("/AdminPanel/RemoveAdmin/{id}")]
        public void Remove(int id)
        {
            _admin.Remove(id);
        }
    }
}
