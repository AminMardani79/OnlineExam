using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Application.ViewModels.GeneralViewModel
{
   public class EditExamViewModel
    {
       [Required(ErrorMessage = "عنوان آزمون را وارد کنید")]
       [MaxLength(250, ErrorMessage = "طول نام آزمون از حد مجاز بیشتر است")]
       public string TestTitle { set; get; }

       [Required(ErrorMessage = "روز آزمون را وارد کنید")]
       public string TestDayTime { set; get; }
       [Required(ErrorMessage = "ساعت شروع آزمون را وارد کنید")]
       public string StartTest { set; get; }
       [Required(ErrorMessage = "ساعت اتمام آزمون را وارد کنید")]
       public string EndTest { set; get; }
       [Required(ErrorMessage = "توضیح آزمون را وارد کنید")]
       [MaxLength(500, ErrorMessage = "طول توضیح آزمون از حد مجاز بیشتر است")]
       public string TestDescription { set; get; }
        [Required(ErrorMessage = "قیمت آزمون را وارد کنید")]
        public double TestPrice { get; set; }
        public IFormFile TestFile { set; get; }
       [Required(ErrorMessage = "لطفا مدت زمان آزمون را مشخص کنید")]
       public string TestDuration { get; set; }
        public  int Id { set; get; }
        public string FilePath { set; get; }

       public int TeacherId { get; set; }

        public bool NegativePoint { get; set; }
        public bool IsComprehensiveTest { get; set; }
        [Required(ErrorMessage = "نام درس الزامی است")]
       public string LessonId { set; get; }

       /*************************/

       [Required(ErrorMessage = "نام پایه تحصیلی الزامی است")]
       public string GradeId { get; set; }
   }
}

