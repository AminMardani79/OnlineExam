using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.AdminViewModel
{
    public class RequestAdminLoginViewModel
    {
        [Required(ErrorMessage = "کد ملی الزامی است")]
        public string AdminCode { set; get; }
        [Required(ErrorMessage = "گذر واژه الزامی است")]
        public string AdminPassword { set; get; }
        public bool SaveMe { set; get; }
    }
}
