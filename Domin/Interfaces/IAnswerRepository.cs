using System.Collections.Generic;
using System.Threading.Tasks;
using Domin.Models;

namespace Domin.Interfaces
{
    public interface IAnswerRepository
    {
        Task<IEnumerable<AnswerModel>> GetAnswersForStudent(int studentId,int testId);
        Task<IEnumerable<AnswerModel>> GetDescriptiveAnswersForStudent(int studentId,int testId);
        string GetAnswerFile(int studentId, int testId);
        Task<bool> IsSubmitAnswer(int studentId,int testId);
        void CreateAnswer(AnswerModel answer);
        Task<bool> IsExistAnswer(int questionNumber);
        void Save();
    }
}