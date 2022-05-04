using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domin.Models
{
    public class StudentModel
    {
        [Key]
        public int StudentId { set; get; }
        [Required(ErrorMessage = "نام دانشجو را وارد کنید")]
        [MaxLength(200,ErrorMessage = "طول نام دانشجو از حد مجاز بیشتر است")]
        public string StudentName { set; get; }
        [Required(ErrorMessage = "پست الکترونیک دانشجو را وارد کنید")]
        [EmailAddress(ErrorMessage = "پست الکترونیک نامعتبر است")]
        public string StudentMail { set; get; }
        public int RoleId { get; set; }
        [Required(ErrorMessage = "شماره موبایل دانشجو را وارد کنید")]
        [RegularExpression(@"^0[0-9]{10}$", ErrorMessage = "شماره موبایل وارد شده معتبر نمیباشد")]
        public string StudentPhoneNumber { set; get; }
        [RegularExpression(@"^[0-9]{10}$", ErrorMessage = "کد ملی وارد شده معتبر نمیباشد")]
        [Required(ErrorMessage = "کدملی دانشجو را وارد کنید")]
        public string StudentNationalCode { set; get; }
        public string StudentAvatar { set; get; }
        [Required(ErrorMessage = "گذر واژه دانشجو را وارد کنید")]
        public string StudentPassword { set; get; }
        public bool ActiveAccount { set; get; }
        public bool IsStudentDelete { set; get; } = false;
        public int GradeId { get; set; }
        public GradeModel Grade { set; get; }
        public ICollection<MasterWithStudentModel> MasterWithStudent { set; get; }
        public IEnumerable<AnswerModel> AnswerModels { get; set; }
        public IEnumerable<WorkBookModel> WorkBookModels { get; set; }
        public IEnumerable<LevelPercentModel> LevelPercentModels { get; set; }
        public IEnumerable<OrderModel> OrderModels { get; set; }
    }
}