using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.AdminDashboardViewModel
{
    public class AdminDashboardViewModel
    {
        public int StudentsCount { get; set; }
        public int WorkBooksCount { get; set; }
        public int TestsCount { get; set; }
        public int ActiveTestsCount { get; set; }
        public double Participated { get; set; }
        public double NotParticipated { get; set; }
        public double FinishedStudents { get; set; }
        public double UnFinishedStudents { get; set; }
    }
}