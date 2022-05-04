using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domin.Models
{
    public class TeacherLessonModel
    {
        [Required]
        public int LessonId { get; set; }
        [Required]
        public int TeacherId { get; set; }
        [ForeignKey("LessonId")]
        public LessonModel LessonModel { get; set; }
        [ForeignKey("TeacherId")]
        public TeacherModel TeacherModel { get; set; }
    }
}