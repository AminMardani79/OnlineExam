using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domin.Models
{
    public class AnswerModel
    {
        [Key]
        public int AnswerId { get; set; }
        public int StudentId { get; set; }
        public int TestId { get; set; }
        [Required][MaxLength(20)]
        public int AnswerNumber { get; set; }
        [MaxLength(20)]
        public string AnswerChecked { get; set; }
        public string AnswerFile{ get; set; }
        public string AnswerContext { get; set; }
        public bool IsDescriptive { get; set; } = false;
        [ForeignKey("StudentId")]
        public StudentModel StudentModel { get; set; }
        [ForeignKey("TestId")]
        public TestModel TestModel { get; set; }
    }
}