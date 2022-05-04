using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.CorrectTestViewModel
{
    public class CorrectTestViewModel
    {
        public int TestCounts { get; set; }
        public int CorrectAnswers { get; set; }
        public int WrongAnswers { get; set; }
        public int NoCheckedAnswers { get; set; }
        public double Percent { get; set; }
        public int Rank { get; set; }
        public string LessonName { get; set; }
        public int StudetnId { get; set; }
        public int TestId { get; set; }
        public double Level { get; set; }
        public int Score { get; set; }
    }
}
