using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.TeacherViewModel
{
    public class TeacherViewModel
    {
        public int TeacherId { get; set; }
        public string TeacherImage { get; set; }
        public string TeacherName { get; set; }
        public string TeacherEmail { get; set; }
        public string PhoneNumber { get; set; }
        public string NationalCode { get; set; }
        public int PageCount { get; set; }
        public int CurrentPage { get; set; }
        public int RoleId { get; set; }
        public int TestsCount { get; set; }
    }
}
