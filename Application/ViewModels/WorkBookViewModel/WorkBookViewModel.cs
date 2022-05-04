using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.WorkBookViewModel
{
    public class WorkBookViewModel
    {
        public string LessonName { get; set; }
        public int TestCounts { get; set; }
        public int CorrectAnswers { get; set; }
        public int WrongAnswers { get; set; }
        public int NoCheckedAnswers { get; set; }
        public double Percent { get; set; }
        public double HighestPercent { get; set; }
        public int Rank { get; set; }
        public double Level { get; set; }
        public double HighestLevel { get; set; }

    }
}