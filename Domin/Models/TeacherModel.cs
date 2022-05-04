﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domin.Models
{
    public class TeacherModel
    {
        [Key]
        public int TeacherId { get; set; }
        [Display(Name = "نام مدرس")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = ("{0} نمیتواند بیشتر از {1} باشد"))]
        public string TeacherName { get; set; }
        [Display(Name = "شماره همراه")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^0[0-9]{10}$", ErrorMessage = "شماره موبایل وارد شده معتبر نمیباشد")]
        public string PhoneNumber { get; set; }
        public int RoleId { get; set; }
        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = ("{0} نمیتواند بیشتر از {1} باشد"))]
        [EmailAddress(ErrorMessage = ("ایمیل معتبر نمی باشد"))]
        public string TeacherEmail { get; set; }
        [Display(Name = "تصویر مدرس")]
        public string TeacherImage { get; set; }
        [Display(Name ="کد ملی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [RegularExpression(@"^[0-9]{10}$", ErrorMessage = "کد ملی وارد شده معتبر نمیباشد")]
        public string NationalCode { get; set; }
        [Required(ErrorMessage = "گذر واژه الزامی است")]
        [MaxLength(100,ErrorMessage = "طول گذر واژه زیاد است")]
        public string Password { set; get; }
        public string About { set; get; }
        public bool IsTeacherDeleted { get; set; } = false;
        public ICollection<MasterWithStudentModel> MasterWithStudent { set; get; }
        public bool ActiveAccount { set; get; }
        public IEnumerable<TestModel> TestModels { set; get; }
        public IEnumerable<TeacherLessonModel> TeacherLessonModels { get; set; }
    }
}