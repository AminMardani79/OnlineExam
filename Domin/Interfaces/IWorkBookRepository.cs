using Domin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domin.Interfaces
{
    public interface IWorkBookRepository
    {
        void CreteWorkBook(WorkBookModel model);
        void EditWorkBook(WorkBookModel model);
        Task<WorkBookModel> GetWorkBookById(int id);
        Task<bool> IsSubmitWorkBook(int studentId,int testId);
        Task<List<WorkBookModel>> GetWorkBookByIds(int studentId,int testId);
        Task<List<WorkBookModel>> GetWorkBookByIds(int testId, string lessonName);
        Task<WorkBookModel> GetHighestPercent(int testId, string lessonName);
        Task<WorkBookModel> GetHighestLevel(int testId,string lessonName);
        Task<int> ParticipantCounts(int testId);
        Task<double> GetAveragePercent(int testId, string lessonName);
        Task<List<double>> GetAllPercents(int testId,string lessonName);
        int GetWorkBooksCount(int testId,string lessonName);
        Task<int> GetWorkBooksCount();
        int GetCorrectsCountById(int testId,int studentId);
        int GetWrongsCountById(int testId, int studentId);
        int GetNoCheckedById(int testId, int studentId);
        Task<IEnumerable<LevelPercentModel>> GetLevelPercentsById(int studentId);
        Task<LevelPercentModel> GetLevelPercentByIds(int testId,int studentId);
        void Save();
    }
}