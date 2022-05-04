using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.GeneralViewModel
{
    public class MasterDashboardViewModel
    {
        public string TeacherName { get; set; }
        public string PhoneNumber { get; set; }
        public string TeacherEmail { get; set; }
        public string NationalCode { get; set; }
        public string Avatar { set; get; }
        public string About { set; get; }
        public int ActiveTestsCount { get; set; }
        public double Participated { get; set; }
        public double NotParticipated { get; set; }
        public double FinishedStudents { get; set; }
        public double UnFinishedStudents { get; set; }
    }
}