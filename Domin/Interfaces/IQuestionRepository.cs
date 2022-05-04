using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domin.Models;

namespace Domin.Interfaces
{
    public interface IQuestionRepository
    {
        Task<IEnumerable<QuestionModel>> GetAllQuestion(int testId);
        Task<IEnumerable<QuestionModel>> GetTestQuestions(int testId);
        Task<IEnumerable<QuestionModel>> GetAllDescriptiveQuestion(int testId);
        Task<QuestionModel> GetQuestionById(int id);
        Task<QuestionModel> GetQuestionByNumber(int answerNumber,int testId,int lessonId);
        void Create(QuestionModel model);
        void Edit(QuestionModel model);
        void Delete(QuestionModel model);
        int GetQuestionCountsByLessonId(int lessonId,int testId);
        Task<IEnumerable<QuestionModel>> GetTestQuestionsByLessonId(int testId,int lessonId);
        Task<QuestionModel> GetQuestionScore(int lessonId);
    }
}