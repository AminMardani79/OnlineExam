using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.QuestionViewModel
{
    public class CreateQuestionViewModel
    {
        [MaxLength(400, ErrorMessage = "طول سوال بیشتر از حد مجاز است")]
        public string Question { set; get; }
        [Required(ErrorMessage = "لطفا شماره سوال را وارد کنید")]
        public int QuestionNumber { get; set; }
        [MaxLength(20, ErrorMessage = "طول این رشته بیش از حد مجاز است")]
        public string TestKeyAnswer { get; set; }
        public bool Descriptive { set; get; }
        [Required(ErrorMessage = "برای سوال نمره را وارد کنید")]
        public string Score { set; get; }
        public int TestId { set; get; }
        public int LessonId { get; set; }
    }
}
