using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domin.Models
{
    public class QuestionModel
    {
        [Key]
        public int Id { set; get; }
        [Required(ErrorMessage = "لطفا شماره سوال را وارد کنید")]
        public int QuestionNumber { get; set; }
        [MaxLength(400,ErrorMessage = "طول سوال بیشتر از حد مجاز است")]
        public string Question { set; get; }
        [MaxLength(20,ErrorMessage = "طول این رشته بیش از حد مجاز است")]
        public string TestKeyAnswer { get; set; }
        public bool Descriptive { set; get; }
        [Required(ErrorMessage = "برای سوال نمره را وارد کنید")]
        public int Score { set; get; }
        public int LessonId { get; set; }
        public int TestId { set; get; }
        [ForeignKey("TestId")]
        public TestModel Test { set; get; }
    }
}
