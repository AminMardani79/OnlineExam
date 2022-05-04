namespace Application.ViewModels.AnswerViewModel
{
    public class AnswerViewModel
    {
        public int AnswerId { get; set; }
        public int StudentId { get; set; }
        public int TestId { get; set; }
        public int AnswerNumber { get; set; }
        public string AnswerChecked { get; set; }
        public string AnswerFile { get; set; }
        public string AnswerContext { get; set; }
    }
}