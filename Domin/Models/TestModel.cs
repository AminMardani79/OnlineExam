using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domin.Models
{
    public class TestModel
    {
        [Key]
        public int TestId { set; get; }
        [Required(ErrorMessage = "عنوان آزمون را وارد کنید")]
        [MaxLength(250, ErrorMessage = "طول نام آزمون از حد مجاز بیشتر است")]
        public string TestTitle { set; get; }
       
        [Required(ErrorMessage = "روز آزمون را وارد کنید")]
        public DateTime TestDayTime { set; get; }
        [Required(ErrorMessage = "ساعت شروع آزمون را وارد کنید")]
        public DateTime StartTest { set; get; }
        [Required(ErrorMessage = "ساعت اتمام آزمون را وارد کنید")]
        public DateTime EndTest { set; get; }
        [Required(ErrorMessage = "توضیح آزمون را وارد کنید")]
        [MaxLength(500, ErrorMessage = "طول توضیح آزمون از حد مجاز بیشتر است")]
        public string TestDescription { set; get; }
        public string TestFile { set; get; }
        [Required(ErrorMessage = "کد آزمون را وارد کنید")]
        [MaxLength(100, ErrorMessage = "طول کد بیش از حد مجاز است")]
        public string TestCode { get; set; }
        [Required(ErrorMessage = "لطفا مدت زمان آزمون را مشخص کنید")]
        public TimeSpan TestDuration { get; set; }
        [Required(ErrorMessage = "تعداد سوال را وارد کنید")]
       
        public bool Finish { set; get; } = false;

        public bool IsComprehensiveTest { get; set; } = false;

        public bool NegativePoint { get; set; } = false;
        [Required(ErrorMessage = "قیمت آزمون را وارد کنید")]
        public double TestPrice { get; set; }
        [Display(Name = "استاد")]
        [Required(ErrorMessage = "نام استاد الزامی است")]
        public int TeacherId { get; set; }
        [ForeignKey("TeacherId")]
        public TeacherModel Teacher { set; get; }
        /*************************/
        [Display(Name = "درس")]
        [Required(ErrorMessage = "نام درس الزامی است")]
        public int LessonId { set; get; }
        [ForeignKey("LessonId")]
        public LessonModel Lesson { set; get; }
        /*************************/
        [Display(Name = "پایه تحصیلی")]
        [Required(ErrorMessage = "نام پایه تحصیلی الزامی است")]
        public int GradeId { get; set; }
        [ForeignKey("GradeId")]
        public GradeModel Grade { set; get; }
        /*************************/
        public IEnumerable<QuestionModel>Question { set; get; }
        public bool IsDelete { set; get; } = false;
        public IEnumerable<TestStudentsModel> TestStudentsModels { get; set; }
        public IEnumerable<AnswerModel> AnswerModels { get; set; }
        public IEnumerable<WorkBookModel> WorkBookModels { get; set; }
        public IEnumerable<LevelPercentModel> LevelPercentModels { get; set; }
        public IEnumerable<OrderDetailModel> OrderDetails { get; set; }

    }
}
