using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.WorkBookViewModel
{
    public class WorkBookInfoViewModel
    {
        public string StudentName { get; set; }
        public string GradeName { get; set; }
        public string TestDayTime { get; set; }
        public int StudentCounts { get; set; }
        public int QuestionsCount { get; set; }
        public int CorrectsCount { get; set; }
        public int WrongsCount { get; set; }
        public int NoCheckedsCount { get; set; }
        public double AverageLevel { get; set; }
        public double AveragePercent { get; set; } 
        public int StudentRank { get; set; } 
        public double HighestLevel { get; set; }
        public int TestId { get; set; }
        public int StudentId { get; set; }

        public string TestTitle { get; set; }
    }
}
