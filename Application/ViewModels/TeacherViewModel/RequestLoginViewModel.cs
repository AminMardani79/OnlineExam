using Application.ViewModels.AdminViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.TeacherViewModel
{
   public class RequestLoginViewModel
    {
        public RequestMasterViewModel LoginMaster { get; set; } 
        public RequestAdminLoginViewModel LoginAdmin { get; set; }
    }
}
