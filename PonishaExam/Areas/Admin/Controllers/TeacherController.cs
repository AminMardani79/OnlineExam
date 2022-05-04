using Application.Interfaces;
using Application.ViewModels.TeacherViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PonishaExam.Helper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace PonishaExam.Areas.Admin.Controllers
{
    [Area("Admin")]
    [AdminAuthorize]
    public class TeacherController : Controller
    {
        private readonly ITeacherService _teacherService;
        private readonly ILessonService _lessonService;
        private readonly ITestService _testService;

        public TeacherController(ITeacherService teacherService, ILessonService lessonService,ITestService testService)
        {
            _teacherService = teacherService;
            _lessonService = lessonService;
            _testService = testService;
        }
        [HttpGet]
        [Route("/AdminPanel/Teachers/{pageId?}")]
        public IActionResult Index(int pageId = 1)
        {
            int pageCount = _teacherService.TeacherPageCount(5);
            ViewBag.PageCount = pageCount;
            ViewBag.PageId = pageId;
            var models = _teacherService.GetTeacherList(pageId,5).Result;
            return View(models);
        }
        [HttpGet]
        [Route("/AdminPanel/Teacher/Create")]
        public IActionResult Create()
        {
            Lessons();
            return View();
        }
        [HttpPost]
        [Route("/AdminPanel/Teacher/Create")]
        public IActionResult Create(AddTeacherViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (_teacherService.IsExistCode(model.NationalCode))
                {
                    ModelState.AddModelError("NationalCode", "کدملی وارد شده تکراری میباشد");
                    return View(model);
                }
                _teacherService.CreateTeacher(model);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                Lessons();
                return View(model);
            }

        }
        [HttpGet]
        [Route("/AdminPanel/Teacher/Edit")]
        public IActionResult Edit(int id)
        {
            var model = _teacherService.GetTeacherById(id).Result;
            Lessons();
            SelectedLessons(id);
            return View(model);
        }
        [HttpPost]
        [Route("/AdminPanel/Teacher/Edit")]
        public IActionResult Edit(EditTeacherViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.OldMail != model.TeacherEmail)
                {
                    if (_teacherService.IsExistCode(model.NationalCode))
                    {
                        Lessons();
                        SelectedLessons(model.TeacherId);
                        ModelState.AddModelError("TeacherEmail", "ایمیل وارد شده تکراری میباشد");
                        return View(model);
                    }
                }
                _teacherService.UpdateTeacher(model);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                Lessons();
                SelectedLessons(model.TeacherId);
                return View(model);
            }
        }
        [HttpGet]
        [Route("/AdminPanel/Teacher/AddDeleteList/{id}")]
        public void AddDeleteList(int id)
        {
            _teacherService.AddToTrash(id);
        }
        [HttpGet]
        [Route("/AdminPanel/Teacher/ShowDeleteList/{pageId?}")]
        public IActionResult DeleteList(int pageId=1)
        {
            int pageCount = _teacherService.DeletedTeachersPageCount(5);
            ViewBag.PageCount = pageCount;
            ViewBag.PageId = pageId;
            var models = _teacherService.GetDeletedTeacherList(pageId,5).Result;
            return View(models);
        }
        [HttpGet]
        [Route("/AdminPanel/Teacher/DeleteDataBase/{id}")]
        public void Delete(int id)
        {
            var teacher = _teacherService.GetDeletedTeacherById(id).Result;
            //Delete Old Image
            if (teacher.TeacherImage != "avatar.jpg")
            {
                string oldPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/TeacherImages",
                    teacher.TeacherImage);
                if (System.IO.File.Exists(oldPath))
                    System.IO.File.Delete(oldPath);
            }
            _teacherService.DeleteTeacher(id);
        }
        [HttpGet]
        [Route("/AdminPanel/Teacher/RestoreDelete/{id}")]
        public void RestoreDelete(int id)
        {
            _teacherService.BackToList(id);
        }

        public void Lessons()
        {
            ViewBag.list = _lessonService.ItemsLessons().Result;
        }

        public void SelectedLessons(int id)
        {
            ViewBag.select = _teacherService.TeacherLessons(id).Result;
        }
    }
}
