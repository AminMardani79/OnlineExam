using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.LessonViewModel
{
    public class EditLessonViewModel
    {
        public int LessonId { set; get; }
        [Required(ErrorMessage = "نام درس الزامی است")]
        [MaxLength(200, ErrorMessage = "طول نام درس از حد مجاز بیشتر است")]
        public string LessonName { set; get; }
        public int GradeId { get; set; }
    }
}