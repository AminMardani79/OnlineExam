using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.WorkBookViewModel
{
    public class ShowWorkBooksViewModel
    {
        public string TestTitle { get; set; }
        public string StudentName { get; set; }
        public string TestDayTime { get; set; }
        public int TestId { get; set; }
        public int StudentId { get; set; }
        public string TestFile { get; set; }
        public string LessonName { get; set; }
        public int TrueAnswers { get; set; }
        public int WrongAnswers { get; set; }
        public int NoCheckedAnswers { get; set; }
        public double Score { get; set; }
        public int QuestionCounts { get; set; }
    }
}