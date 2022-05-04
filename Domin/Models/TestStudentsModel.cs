using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domin.Models
{
    public class TestStudentsModel
    {
        [Key]
        public int TestStudentsId { get; set; }
        public int StudentId { get; set; }
        public int TestId { get; set; }
        public bool IsShowingWorkBook { get; set; } = false;
        public bool IsSubmitAnswer { get; set; } = false;
        public int EnterCount { get; set; }
        [ForeignKey("TestId")]
        public TestModel TestModel { get; set; }
    }
}