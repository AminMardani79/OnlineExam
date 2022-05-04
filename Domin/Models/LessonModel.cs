using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domin.Models
{
    public class LessonModel
    {
        [Key]
        public int LessonId { set; get; }
        [Required(ErrorMessage = "نام درس الزامی است")]
        [MaxLength(200,ErrorMessage = "طول نام درس از حد مجاز بیشتر است")]
        public string LessonName { set; get; }
        public bool IsLessonDelete { get; set; } = false;
        public int GradeId { get; set; }
        [ForeignKey("GradeId")]
        public GradeModel Grade { set; get; }
        public IEnumerable<TeacherLessonModel> TeacherLessonModels { get; set; }
        public IEnumerable<TestModel>TestModels { set; get; }
    }
}