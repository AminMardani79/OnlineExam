using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.GeneralViewModel
{
    public class ExamMasterViewModel
    {
        public int TestId { set; get; }
        public string TestTitle { set; get; }
        public string StartTime { set; get; }
        public string EndTime { set; get; }
        public string TestDayTime { set; get; }
        public string TestFile { set; get; }
        public string TestCode { get; set; }
        public int QuestionCounts { get; set; }
        public int GradeId { get; set; }
        public int LessonId { get; set; }
        public string TestDuration { get; set; }
        public string TestPrice { get; set; }
    }
}