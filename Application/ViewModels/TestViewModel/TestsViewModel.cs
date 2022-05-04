using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.TestViewModel
{
    public class TestsViewModel
    {
        public int TestId { set; get; }
        public string TestTitle { set; get; }
        public string StartTime { set; get; }
        public string EndTime { set; get; }
        public string TestDayTime { set; get; }
        public string TestFile { get; set; }
        public string TestCode { get; set; }
        public int QuestionCounts { get; set; }
        public int GradeId { get; set; }
        public string TestDuration { get; set; }
        public bool IsSubmitAnswer { get; set; }
        public bool IsStartedExam { get; set; }
        public bool IsFinish { get; set; }
        public int LessonId { get; set; }
        public string TestPrice { get; set; }
        public bool IsBuyTest { get; set; }


    }
}