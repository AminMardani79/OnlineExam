using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.TeacherViewModel
{
    public class RequestMasterViewModel
    {
        [Required(ErrorMessage = "کد ملی الزامی است")]
        public string Code { set; get; }
        [Required(ErrorMessage = "گذر واژه الزامی است")]
        public string Password { set; get; }
        public bool SaveMe { set; get; }
    }
}
