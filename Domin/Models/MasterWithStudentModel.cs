using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domin.Models
{
    public class MasterWithStudentModel
    {
        [Required]
        public int TeacherId { get; set; }
        [Required]
        public int StudentId { set; get; }
        /***********************************/
        [ForeignKey("TeacherId")]
        public TeacherModel Teacher { set; get; }
        [ForeignKey("StudentId")]
        public StudentModel Student { set; get; }
    }
}
