using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.LessonViewModel
{
    public class LessonViewModel
    {
        public int LessonId { get; set; }
        public string LessonName { get; set; }
        public string GradeName { get; set; }
        public int TestsCount { get; set; } 

    }
}
