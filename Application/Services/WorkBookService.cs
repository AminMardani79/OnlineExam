using Application.Interfaces;
using Application.ViewModels.CorrectTestViewModel;
using Application.ViewModels.WorkBookViewModel;
using Domin.Interfaces;
using Domin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class WorkBookService : IWorkBookService
    {
        private readonly IWorkBookRepository _workBookRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly ITestRepository _testRepository;
        private readonly ILevelPercentRepository _levelPercentRepository;
        public WorkBookService(IWorkBookRepository workBookRepository,IStudentRepository studentRepository,ITestRepository testRepository,ILevelPercentRepository levelPercentRepository)
        {
            _workBookRepository = workBookRepository;
            _studentRepository = studentRepository;
            _testRepository = testRepository;
            _levelPercentRepository = levelPercentRepository;
        }
        public void CreateWorkBook(List<CorrectTestViewModel> models,int testId,int studentId)
        {
            double level;
            int particiPantsCount = ParticipantCounts(testId);
            List<double> lessonLevels = new List<double>();
            List<double> lessonPercents = new List<double>();
            List<int> lessonScores = new List<int>();
            foreach (var model in models)
            {
                CalculateLevel(testId,model.LessonName,model.Percent,particiPantsCount,out level);
                WorkBookModel wbModel = new WorkBookModel();
                wbModel.LessonName = model.LessonName;
                wbModel.NoCheckedAnswers = model.NoCheckedAnswers;
                wbModel.Percent = model.Percent;
                wbModel.QuestionCounts = model.TestCounts;
                wbModel.StudentId = model.StudetnId;
                wbModel.TestId = model.TestId;
                wbModel.TrueAnswers = model.CorrectAnswers;
                wbModel.WrongAnswers = model.WrongAnswers;
                wbModel.Level = level;
                wbModel.LessonScore = model.Score;
                lessonLevels.Add(level);
                lessonPercents.Add(model.Percent);
                lessonScores.Add(model.Score);
                _workBookRepository.CreteWorkBook(wbModel);
            }
            double averageScore = CalculateScore(lessonPercents, lessonScores);
            double averageLevel = CalculateAverageLevel(lessonLevels,lessonScores);
            LevelPercentModel lpModel = new LevelPercentModel()
            {
                TestLevel = averageLevel,
                TestScore = averageScore,
                StudentId = studentId,
                TestId = testId
            };
            _levelPercentRepository.CreateLevelPercent(lpModel);
        }
        public void EditWorkBook(List<CorrectTestViewModel> models,int testId,int studentId)
        {
            int index = 0;
            double level;
            List<double> lessonLevels = new List<double>();
            List<double> lessonPercents = new List<double>();
            List<int> lessonScores = new List<int>();
            int particiPantsCount = ParticipantCounts(testId);
            foreach (var model in models)
            {
                CalculateLevel(testId, model.LessonName, model.Percent, particiPantsCount, out level);
                var workBooks = _workBookRepository.GetWorkBookByIds(model.StudetnId,model.TestId).Result;
                workBooks[index].LessonName = model.LessonName;
                workBooks[index].NoCheckedAnswers = model.NoCheckedAnswers;
                workBooks[index].Percent = model.Percent;
                workBooks[index].QuestionCounts = model.TestCounts;
                workBooks[index].StudentId = model.StudetnId;
                workBooks[index].TestId = model.TestId;
                workBooks[index].TrueAnswers = model.CorrectAnswers;
                workBooks[index].WrongAnswers = model.WrongAnswers;
                workBooks[index].Level = level;
                workBooks[index].LessonScore = model.Score;
                lessonLevels.Add(level);
                lessonPercents.Add(model.Percent);
                lessonScores.Add(model.Score);
                _workBookRepository.EditWorkBook(workBooks[index]);
                var lpModel = _levelPercentRepository.GetLevelPercentByIds(studentId, testId).Result;
                lpModel.TestId = testId;
                lpModel.StudentId = studentId;
                lpModel.TestLevel = CalculateAverageLevel(lessonLevels, lessonScores);
                lpModel.TestScore = CalculateScore(lessonPercents, lessonScores);
                _levelPercentRepository.UpdateLevelPercent(lpModel);
                index++;
            }
        }
        public Tuple<List<WorkBookViewModel>, WorkBookInfoViewModel> ShowWorkBookInfo(int studentId, int testId)
        {
            var workBooks = _workBookRepository.GetWorkBookByIds(studentId,testId).Result;
            var student = _studentRepository.GetStudentById(studentId).Result;
            int particiPantsCount = ParticipantCounts(testId);
            var test = _testRepository.GetTestById(testId).Result;
            double level;
            List<double> lessonLevels = new List<double>();
            List<double> lessonPercents = new List<double>();
            List<int> lessonScores = new List<int>();
            List<WorkBookViewModel> models = new List<WorkBookViewModel>();
            foreach (var workBook in workBooks)
            {
                lessonScores.Add(workBook.LessonScore);
                lessonLevels.Add(workBook.Level);
                lessonPercents.Add(workBook.Percent);
                CalculateLevel(testId,workBook.LessonName,workBook.Percent,particiPantsCount,out level);
                UpdateLevel(workBook.WorkBookId,level);
                var workBookList = _workBookRepository.GetWorkBookByIds(testId,workBook.LessonName).Result;
                var highestPercent = _workBookRepository.GetHighestPercent(testId,workBook.LessonName).Result;
                var highestLevel = _workBookRepository.GetHighestLevel(testId,workBook.LessonName).Result;
                int rank = workBookList.FindIndex(n=> n.WorkBookId == workBook.WorkBookId) + 1;
                models.Add(new WorkBookViewModel() 
                {
                    LessonName = workBook.LessonName,
                    TestCounts = workBook.QuestionCounts,
                    CorrectAnswers = workBook.TrueAnswers,
                    WrongAnswers = workBook.WrongAnswers,
                    NoCheckedAnswers = workBook.NoCheckedAnswers,
                    Percent = workBook.Percent,
                    HighestPercent = highestPercent.Percent,
                    Rank = rank,
                    Level = level,
                    HighestLevel = highestLevel.Level
                });
            }
            int questionsCount = _testRepository.GetAllQuestionCountsById(testId);
            int correctsCount = _workBookRepository.GetCorrectsCountById(testId, studentId);
            int wrongsCount = _workBookRepository.GetWrongsCountById(testId, studentId);
            int noCheckedsCount = _workBookRepository.GetNoCheckedById(testId, studentId);
            var lpModels = _levelPercentRepository.GetRanksByDecending(testId).Result;
            int studentRank = lpModels.FindIndex(n=> n.StudentId == studentId) + 1;
            var highestLevelForModel = _levelPercentRepository.GetHighestAverageLevel(testId).Result;
            WorkBookInfoViewModel model = new WorkBookInfoViewModel();
            model.StudentName = student.StudentName;
            model.GradeName = student.Grade.GradeName;
            model.StudentCounts = particiPantsCount;
            model.TestDayTime = test.TestDayTime.ToString("yyyy/MM/dd");
            model.CorrectsCount = correctsCount;
            model.WrongsCount = wrongsCount;
            model.NoCheckedsCount = noCheckedsCount;
            model.QuestionsCount = questionsCount;
            model.AverageLevel = CalculateAverageLevel(lessonLevels, lessonScores);
            model.AveragePercent = CalculateScore(lessonPercents,lessonScores);
            model.StudentRank = studentRank;
            model.HighestLevel = highestLevelForModel.TestLevel;
            model.StudentId = studentId;
            model.TestId = testId;
            model.TestTitle = test.TestTitle;
            return Tuple.Create(models,model);
        }
        public void UpdateLevel(int workBookId, double level)
        {
            var model = _workBookRepository.GetWorkBookById(workBookId).Result;
            model.Level = level;
            _workBookRepository.EditWorkBook(model);
        }
        public double CalculateLevel(int testId,string lessonName,double percent,int participantCounts, out double level)
        {
            var workBooksCount = _workBookRepository.GetWorkBooksCount(testId,lessonName);
            if (workBooksCount is not 0)
            {
                double Z = 0;
                double S = 0; // Enheraf Meyar
                double subtractEachPercent = 0;
                var averagePercent = _workBookRepository.GetAveragePercent(testId, lessonName).Result;
                List<double> percents = _workBookRepository.GetAllPercents(testId, lessonName).Result;
                var percentSubtract = percent - averagePercent;
                foreach (var eachPercent in percents)
                {
                    subtractEachPercent += Math.Pow((eachPercent - averagePercent), 2);
                }
                S = Math.Sqrt((subtractEachPercent / participantCounts));
                Z = percentSubtract / S;
                level = (2000 * Z) + 5000;
                if (S is 0)
                {
                    level = 0;
                    return level;
                }
                else
                {
                    return level;
                }
            }
            else
            {
                level = 0;
                return level;
            }
        }
        public double CalculateAverageLevel(List<double> lessonLevels,List<int> lessonScores)
        {
            var sumScores = lessonScores.Sum();
            double levelScore = 0;
            for (int i=0;i<lessonLevels.Count;i++)
            {
                levelScore += (lessonLevels[i]*lessonScores[i]);
            }
            double averageLevel = levelScore / sumScores;
            return averageLevel;
        }
        public double CalculateScore(List<double> lessonPercents, List<int> lessonScores)
        {
            var sumScores = lessonScores.Sum();
            double percentScore = 0;
            for (int i = 0; i < lessonPercents.Count; i++)
            {
                percentScore += (lessonPercents[i] * lessonScores[i]);
            }
            double averagePercents = percentScore / sumScores;
            return averagePercents;
        }
        public int ParticipantCounts(int testId)
        {
            int particiPantsCount = _workBookRepository.ParticipantCounts(testId).Result;
            return particiPantsCount;
        }
    }
}
