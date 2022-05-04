using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domin.Models
{
    public class WorkBookModel
    {
        [Key]
        public int WorkBookId { get; set; }
        [Display(Name ="دانش آموز")]
        public int StudentId { get; set; }
        [Display(Name = "آزمون")]
        public int TestId { get; set; }
        [Required(ErrorMessage ="نام درس را وارد کنید")]
        [MaxLength(250,ErrorMessage ="طول رشته بیش از حد مجاز است")]
        public string LessonName { get; set; }
        [Required(ErrorMessage = "تعداد سوال را وارد کنید")]
        public int QuestionCounts { get; set; }
        [Required(ErrorMessage = "تعداد پاسخ های صحیح را وارد کنید")]
        public int TrueAnswers { get; set; }
        [Required(ErrorMessage = "تعداد پاسخ های غلط را وارد کنید")]
        public int WrongAnswers { get; set; }
        [Required(ErrorMessage = "تعداد سوالات بی جواب را وارد کنید")]
        public int NoCheckedAnswers { get; set; }
        [Required(ErrorMessage = "درصد کل را وارد کنید")]
        public double Percent { get; set; }
        [Required(ErrorMessage = "رتبه را وارد کنید")]
        public int Rank { get; set; }
        public double Level { get; set; }
        public int LessonScore { get; set; }


        [ForeignKey("StudentId")]
        public StudentModel StudentModel { get; set; }
        [ForeignKey("TestId")]
        public TestModel TestModel { get; set; }
    }
}