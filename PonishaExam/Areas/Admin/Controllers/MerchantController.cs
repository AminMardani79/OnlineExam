using Application.Interfaces;
using Application.ViewModels.MerchantViewModel;
using Microsoft.AspNetCore.Mvc;
using PonishaExam.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PonishaExam.Areas.Admin.Controllers
{
    [Area("Admin")]
    [AdminAuthorize]
    public class MerchantController : Controller
    {
        private readonly IMerchantService _merchantService;
        public MerchantController(IMerchantService merchantService)
        {
            _merchantService = merchantService;
        }
        [HttpGet]
        [Route("/Admin/EditMerchant")]
        public IActionResult EditMerchant()
        {
            var merchant = _merchantService.GetFirshMerchant().Result;
            return View(merchant);
        }
        [HttpPost]
        [Route("/Admin/EditMerchant")]
        public IActionResult EditMerchant(EditMerchantViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            _merchantService.UpdateMerchant(model);
            return RedirectToAction("EditMerchant");
        }
    }
}
