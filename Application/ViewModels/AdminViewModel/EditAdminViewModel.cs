using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.AdminViewModel
{
    public class EditAdminViewModel
    {
        public int AdminId { get; set; }
        public IFormFile Avatar { set; get; }
        public string AdminImage { get; set; } 
        [Display(Name = "نام مدیر")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = ("{0} نمیتواند بیشتر از {1} باشد"))]
        public string AdminName { get; set; }
        [Display(Name = "شماره همراه")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^0[0-9]{10}$", ErrorMessage = "شماره موبایل وارد شده معتبر نمیباشد")]
        public string PhoneNumber { get; set; }
        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = ("{0} نمیتواند بیشتر از {1} باشد"))]
        [EmailAddress(ErrorMessage = ("ایمیل معتبر نمی باشد"))]
        public string AdminEmail { get; set; }
        [Display(Name = "کد ملی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [RegularExpression(@"^[0-9]{10}$", ErrorMessage = "کد ملی وارد شده معتبر نمیباشد")]
        public string NationalCode { get; set; }
        [MaxLength(100, ErrorMessage = "طول گذر واژه زیاد است")]
        public string Password { set; get; }
        public string OldPassword { set; get; }
        public string About { set; get; }
        public string OldCode { set; get; }
        public bool ActiveAccount { set; get; }
        public bool IsAdminDeleted { get; set; } 
    }
}
