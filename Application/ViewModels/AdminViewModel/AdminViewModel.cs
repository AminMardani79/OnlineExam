using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.AdminViewModel
{
    public class AdminViewModel
    {
        public int AdminId { get; set; }
        public string AdminImage { get; set; }
        public string AdminName { get; set; }
        public string AdminEmail { get; set; }
        public string PhoneNumber { get; set; }
        public string NationalCode { get; set; }
       
        public int RoleId { get; set; }
    }
}
