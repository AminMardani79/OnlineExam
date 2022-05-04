using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domin.Models;
using Microsoft.AspNetCore.Http;

namespace Application.ViewModels.TestViewModel
{
    public class AddTestViewModel
    {
        [Required(ErrorMessage = "عنوان آزمون را وارد کنید")]
        [MaxLength(250, ErrorMessage = "طول نام آزمون از حد مجاز بیشتر است")]
        public string TestTitle { set; get; }
       
        [Required(ErrorMessage = "روز آزمون را وارد کنید")]
        public DateTime TestDayTime { set; get; }
        [Required(ErrorMessage = "ساعت شروع آزمون را وارد کنید")]
        public string StartTest { set; get; }
        [Required(ErrorMessage = "ساعت اتمام آزمون را وارد کنید")]
        public string EndTest { set; get; }
        [Required(ErrorMessage = "توضیح آزمون را وارد کنید")]
        [MaxLength(500, ErrorMessage = "طول توضیح آزمون از حد مجاز بیشتر است")]
        public string TestDescription { set; get; }
        [Required(ErrorMessage = "فایل آزمون را بارگذاری کنید")]
        public IFormFile TestFile { set; get; }
        public string TestCode { get; set; }
        [Required(ErrorMessage = "قیمت آزمون را وارد کنید")]
        public double TestPrice { get; set; }
        [Display(Name = "استاد")]
        [Required(ErrorMessage = "نام استاد الزامی است")]
        public int TeacherId { get; set; }

        [Display(Name = "درس")]
        [Required(ErrorMessage = "نام درس الزامی است")]
        public int LessonId { set; get; }
        [Required(ErrorMessage = "لطفا مدت زمان آزمون را مشخص کنید")]
        public string TestDuration { get; set; }
        public bool NegativePoint { get; set; }
        public bool IsComprehensiveTest { get; set; }

        /*************************/
        [Display(Name = "پایه تحصیلی")]
        [Required(ErrorMessage = "نام پایه تحصیلی الزامی است")]
        public int GradeId { get; set; }
     
    }
}
