using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Application.ViewModels.StudentViewModel
{
    public class EditStudentViewModel
    {
        public int StudentId { set; get; }
        [Required(ErrorMessage = "نام دانشجو را وارد کنید")]
        [MaxLength(200, ErrorMessage = "طول نام دانشجو از حد مجاز بیشتر است")]
        public string StudentName { set; get; }
        [Required(ErrorMessage = "پست الکترونیک دانشجو را وارد کنید")]
        [EmailAddress(ErrorMessage = "پست الکترونیک نامعتبر است")]
        public string StudentMail { set; get; }
        [Required(ErrorMessage = "شماره موبایل دانشجو را وارد کنید")]
        [RegularExpression(@"^0[0-9]{10}$", ErrorMessage = "شماره موبایل وارد شده معتبر نمیباشد")]
        public string StudentPhoneNumber { set; get; }
        [RegularExpression(@"^[0-9]{10}$", ErrorMessage = "کد ملی وارد شده معتبر نمیباشد")]
        [Required(ErrorMessage = "کدملی دانشجو را وارد کنید")]
        public string StudentNationalCode { set; get; }
        public bool Active { set; get; }
        public IFormFile StudentAvatar { set; get; }
        public string StudentPathImage { set; get; }
        public string StudentPassword { set; get; }
        public string StudentOldPassword { set; get; }
        public int GradeId { get; set; }
     
    }
}