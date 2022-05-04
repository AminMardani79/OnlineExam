using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.GradeViewModel
{
    public class AddGradeViewModel
    {
        [MaxLength(200, ErrorMessage = ("عنوان نمیتواند بیشتر از200کارتر باشد"))]
        [Required(ErrorMessage = "لطفا عنوان را وارد کنید")]
        public string GradeName { get; set; }
    }
}
