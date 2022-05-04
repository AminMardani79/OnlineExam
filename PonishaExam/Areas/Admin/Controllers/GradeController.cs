using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Interfaces;
using Application.ViewModels.GradeViewModel;
using PonishaExam.Helper;

namespace PonishaExam.Areas.Admin.Controllers
{
    [Area("Admin")]
    [AdminAuthorize]
    public class GradeController : Controller
    {
        private readonly IGradeService _grade;

        public GradeController(IGradeService grade)
        {
            _grade = grade;
        }
        [HttpGet]
        [Route("/AdminPanel/Grade")]
        public IActionResult Index()
        {
            var models = _grade.GetAllGrades().Result;
            return View(models);
        }
        [HttpGet]
        [Route("/AdminPanel/Grade/Create")]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [Route("/AdminPanel/Grade/Create")]
        public IActionResult Create(AddGradeViewModel model)
        {
            if (ModelState.IsValid)
            {
                _grade.AddGrade(model);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(model);
            }

        }
        [HttpGet]
        [Route("/AdminPanel/Grade/Edit")]
        public IActionResult Edit(int id)
        {
            var model = _grade.GetGradeById(id).Result;
            return View(model);
        }
        [HttpPost]
        [Route("/AdminPanel/Grade/Edit")]
        public IActionResult Edit(EditGradeViewModel model)
        {
            if (ModelState.IsValid)
            {
                _grade.UpdateGrade(model);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(model);

            }
        }
        //Put model in delete list
        [HttpGet]
        [Route("/AdminPanel/Grade/AddDeleteList/{id}")]
        public void AddDeleteList(int id)
        {
           _grade.AddToTrash(id);
        }
        [HttpGet]
        [Route("/AdminPanel/Grade/ShowDeleteList")]
        public IActionResult DeleteList()
        {
            var models = _grade.GetAllDeletedGrades().Result;
            return View(models);
        }
        //Finally Delete
        [HttpGet]
        [Route("/AdminPanel/Grade/DeleteDataBase/{id}")]
        public void Delete(int id)
        {
            _grade.DeleteGrade(id);
        }
        [HttpGet]
        [Route("/AdminPanel/Grade/RestoreDelete/{id}")]
        public void RestoreDelete(int id)
        {
            _grade.BackToList(id);
        }
    }
}
