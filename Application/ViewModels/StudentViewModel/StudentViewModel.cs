using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.StudentViewModel
{
    public class StudentViewModel
    {
        public int StudentId { set; get; }
        public string StudentName { set; get; }
        public string StudentImage { set; get; }
        public string StudentNationalCode { set; get; }
        public string GradeName { get; set; }
        public bool Active { set; get; }
        public string StudentMail { set; get; }
        public string StudentPhoneNumber { set; get; }
        public string StudentPassword { set; get; }
        public int GradeId { get; set; }
        public int RoleId { get; set; }

    }
}
