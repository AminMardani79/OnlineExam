using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using PonishaExam.Helper;

namespace PonishaExam.Areas.Student.Components
{
    public class LessonViewComponent: ViewComponent
    {
        private readonly ILessonService _lessonService;

        public LessonViewComponent(ILessonService lessonService)
        {
            _lessonService = lessonService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var gradeId = int.Parse(UserClaimsPrincipal.GetGradeId());
            return View("/Views/Shared/StudentViews/_LessonList.cshtml",await _lessonService.GetLessonsByGradeId(gradeId));
        }
    }
}