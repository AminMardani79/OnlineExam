using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.TeacherViewModel
{
    public class TeacherLoginViewModel
    {
        public int TeacherId { get; set; }
        public string TeacherImage { get; set; }
        public string TeacherName { get; set; }
        public string TeacherEmail { set; get; }
        public int RoleId { get; set; }
        public string NationalCode { get; set; }
    }
}
