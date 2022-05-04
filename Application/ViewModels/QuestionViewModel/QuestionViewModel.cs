using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.QuestionViewModel
{
    public class QuestionViewModel
    {
        public int Id { set; get; }
        public string QuestionNumber { set; get; }
        public string KeyAnswer { set; get; }
        public int Score { set; get; }
        public bool Type { set; get; }
        public string QuestionContext { get; set; }
        public int LessonId { get; set; }
    }
}
