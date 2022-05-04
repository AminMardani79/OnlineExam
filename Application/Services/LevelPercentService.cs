using Application.Interfaces;
using Application.ViewModels.LevelPercentViewModel;
using Domin.Interfaces;
using Domin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class LevelPercentService:ILevelPercentService
    {
        private readonly ILevelPercentRepository _levelPercentRepository;
        private readonly ITestRepository _testRepository;
        private readonly IWorkBookRepository _workBookRepository;
        private readonly IWorkBookService _workBookService;

        public LevelPercentService(ILevelPercentRepository levelPercentRepository,ITestRepository testRepository,IWorkBookRepository workBookRepository,IWorkBookService workBookService)
        {
            _levelPercentRepository = levelPercentRepository;
            _testRepository = testRepository;
            _workBookRepository = workBookRepository;
            _workBookService = workBookService;
        }

        public void CreateLevelPercent(AddLevelPercentViewModel model)
        {
            LevelPercentModel lpModel = new LevelPercentModel()
            {
                StudentId = model.StudentId,
                TestId = model.TestId,
                TestScore = model.TestScore,
                TestLevel = model.TestLevel
            };
            _levelPercentRepository.CreateLevelPercent(lpModel);
        }

        public void UpdateLevelPercent(EditLevelPercentViewModel model)
        {
            var lpModel = _levelPercentRepository.GetModelById(model.LevelPercentId).Result;
            lpModel.StudentId = model.StudentId;
            lpModel.TestId = model.TestId;
            lpModel.TestScore = model.TestScore;
            lpModel.TestLevel = model.TestLevel;
            _levelPercentRepository.UpdateLevelPercent(lpModel);
        }
        public void UpdateAllParticipantsInfo(int testId)
        {
            var participants = _testRepository.GetParticipants(testId).Result.ToList();
            int participantsCount = _workBookRepository.ParticipantCounts(testId).Result;
            
            foreach (var participant in participants)
            {
                double level;
                List<double> lessonLevels = new List<double>();
                List<double> lessonPercents = new List<double>();
                List<int> lessonScores = new List<int>();
                var wbModel = _workBookRepository.GetWorkBookByIds(participant.StudentId,testId).Result;
                var lpModel = _levelPercentRepository.GetLevelPercentByIds(participant.StudentId,testId).Result;
                foreach (var model in wbModel)
                {
                    lessonLevels.Add(model.Level);
                    lessonPercents.Add(model.Percent);
                    lessonScores.Add(model.LessonScore);
                    _workBookService.CalculateLevel(model.TestId,model.LessonName,model.Percent, participantsCount,out level);
                    model.Level = level;
                    _workBookRepository.EditWorkBook(model);
                }
                lpModel.TestLevel = _workBookService.CalculateAverageLevel(lessonLevels, lessonScores);
                _levelPercentRepository.UpdateLevelPercent(lpModel);
            }
        }
    }
}
