using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Application.Interfaces;
using Application.ViewModels.GeneralViewModel;
using PonishaExam.Helper;

namespace PonishaExam.Areas.Master.Controllers
{
    [Area("Master")]
    [MasterAuthorize]
    public class MasterDashboardController : Controller
    {
        private readonly IMasterDashboardService _master;

        public MasterDashboardController(IMasterDashboardService master)
        {
            _master = master;
        }

        [HttpGet]
        [Route("/Master")]
        public IActionResult Index()
        {
            var model = _master.GetDashboardViewModel(MasterId());
            return View(model);
        }

        public int MasterId()
        {
            int userId = int.Parse(User.GetTeacherId());
            return userId;
        }

        [HttpGet]
        [Route("/Master/Edit")]
        public IActionResult Edit()
        {
            var model = _master.GetMasterInfoById(MasterId()).Result;
            return View(model);
        }

        [HttpPost]
        [Route("/Master/Edit")]
        public IActionResult Edit(EditMasterViewModel model)
        {
            if (ModelState.IsValid)
            {
                _master.UpdateProfile(model);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(model);
            }
        }
    }
}
