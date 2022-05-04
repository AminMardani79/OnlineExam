using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domin.Models
{
    public class LevelPercentModel
    {
        [Key]
        public int LevelPercentId { get; set; }
        public int StudentId { get; set; }
        public int TestId { get; set; }
        [MaxLength(100)]
        public double TestLevel { get; set; }
        [MaxLength(100)]
        public double TestScore { get; set; }
        [ForeignKey("StudentId")]
        public StudentModel StudentModel { get; set; }
        [ForeignKey("TestId")]
        public TestModel TestModel { get; set; } 

    }
}
