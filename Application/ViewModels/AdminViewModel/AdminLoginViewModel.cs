using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.AdminViewModel
{
    public class AdminLoginViewModel
    {
        public int AdminId { get; set; }
        public string AdminImage { get; set; }
        public string AdminName { get; set; }
        public string AdminEmail { set; get; }
        public int RoleId { get; set; }
        public string AdminNationalCode { get; set; }
    }
}