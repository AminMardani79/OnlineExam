using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Interfaces;
using Application.ViewModels.GradeViewModel;
using PonishaExam.Helper;
using Application.ViewModels.AdminViewModel;

namespace PonishaExam.Areas.Admin.Controllers
{
    [Area("Admin")]
    [AdminAuthorize]
    public class DashboardController : Controller
    {
        private readonly IDashboardService _dashboardService;
        private readonly IAdminService _adminService; 
        public DashboardController(IDashboardService dashboardService,IAdminService adminService)
        {
            _dashboardService = dashboardService;
            _adminService = adminService;
        }
        [HttpGet]
        [Route("/AdminDashboard")]
        public IActionResult Dashboard()
        {
            var dashboard = _dashboardService.GetActiveTestList();
            return View(dashboard);
        }
        [HttpGet]
        [Route("/AdminProfile")]
        public IActionResult ShowAdminProfile()
        {
            int adminId = int.Parse(User.GetAdminId());
            var admin = _adminService.GetAdminProfile(adminId).Result;
            return View(admin);
        }
        [HttpGet]
        [Route("/EditAdminProfile")]
        public IActionResult EditAdminProfile()
        {
            int adminId = int.Parse(User.GetAdminId());
            var admin = _adminService.GetAdminById(adminId).Result;
            return View(admin);
        }
        [HttpPost]
        [Route("/EditAdminProfile")]
        public IActionResult EditAdminProfile(EditAdminViewModel model)
        {
            if (ModelState.IsValid)
            {
                _adminService.Update(model);
                return Redirect("/AdminProfile");
            }
            else
            {
                return View(model);
            }
        }
    }
}
