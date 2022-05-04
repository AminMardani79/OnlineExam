using System.ComponentModel.DataAnnotations;

namespace Application.ViewModels.AccountViewModel
{
    public class LoginStudentViewModel
    {
        [RegularExpression(@"^[0-9]{10}$", ErrorMessage = "کد ملی وارد شده معتبر نمیباشد")]
        [Required(ErrorMessage = "کدملی دانش آموز را وارد کنید")]
        public string StudentNationalCode { get; set; }
        [Required(ErrorMessage = "گذر واژه دانش آموز را وارد کنید")]
        public string StudentPassword { get; set; }
        public bool RememberMe { get; set; }
    }
}