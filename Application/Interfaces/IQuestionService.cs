using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.ViewModels.QuestionViewModel;

namespace Application.Interfaces
{
    public interface IQuestionService
    {
        Tuple<List<QuestionViewModel>, int, int> GetAllQuestions(int id,int page);
        void AddQuestion(CreateQuestionViewModel model);
        void EditQuestion(EditQuestionViewModel model);
        Task<EditQuestionViewModel> GetQuestionById(int id);
        Task<int> GetQuestionScore(string lessonName);
        void Delete(int id);
    }
}
