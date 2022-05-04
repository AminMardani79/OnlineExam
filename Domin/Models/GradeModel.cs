using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domin.Models
{
    public class GradeModel
    {
        [Key]
        public int GradeId { get; set; }
        [Display(Name ="پایه")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = ("{0} نمیتواند بیشتر از {1} باشد"))]
        public string GradeName { get; set; }
        public bool IsGradeDelete { get; set; } = false;
        public IEnumerable<LessonModel> Lessons { set; get; }
        public IEnumerable<StudentModel>Students { set; get; }
        public IEnumerable<TestModel> TestModels { set; get; }
    }
}
