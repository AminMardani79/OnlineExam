using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Application.ViewModels.TeacherViewModel
{
    public class EditTeacherViewModel
    {
        public int TeacherId { get; set; }
        public IFormFile Avatar { set; get; }
        public string TeacherImage { get; set; }
        [Display(Name = "نام مدرس")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = ("{0} نمیتواند بیشتر از {1} باشد"))]
        public string TeacherName { get; set; }
        [Display(Name = "شماره همراه")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^0[0-9]{10}$", ErrorMessage = "شماره موبایل وارد شده معتبر نمیباشد")]
        public string PhoneNumber { get; set; }
        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = ("{0} نمیتواند بیشتر از {1} باشد"))]
        [EmailAddress(ErrorMessage = ("ایمیل معتبر نمی باشد"))]
        public string TeacherEmail { get; set; }
        [Display(Name = "کد ملی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [RegularExpression(@"^[0-9]{10}$", ErrorMessage = "کد ملی وارد شده معتبر نمیباشد")]
        public string NationalCode { get; set; }
        [MaxLength(100, ErrorMessage = "طول گذر واژه زیاد است")]
        public string Password { set; get; }
        public string OldPassword { set; get; }
        public string About { set; get; }
        public string OldMail { set; get; }
        public bool ActiveAccount { set; get; }
        public bool IsTeacherDeleted { get; set; }
        public List<int> Items { set; get; }
       
    }
}