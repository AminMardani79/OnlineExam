using Application.ViewModels.CorrectTestViewModel;
using Application.ViewModels.WorkBookViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IWorkBookService
    {
        void CreateWorkBook(List<CorrectTestViewModel> models, int testId, int studentId);
        void EditWorkBook(List<CorrectTestViewModel> models,int testId,int studentId);
        Tuple<List<WorkBookViewModel>, WorkBookInfoViewModel> ShowWorkBookInfo(int studentId,int testId);
        void UpdateLevel(int workBookId,double level);
        int ParticipantCounts(int testId);
        double CalculateLevel(int testId, string lessonName, double percent, int participantCounts, out double level);
        double CalculateAverageLevel(List<double> lessonLevels, List<int> lessonScores);
        double CalculateScore(List<double> lessonPercents,List<int> lessonScores);
    }
}
