using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Interfaces;
using Application.Others;
using Application.ViewModels.AnswerViewModel;
using Application.ViewModels.TestViewModel;
using Domin.Interfaces;
using Domin.Models;
using Microsoft.AspNetCore.Http;

namespace Application.Services
{
    public class AnswerService : IAnswerService
    {
        private readonly IAnswerRepository _answerRepository;
        private readonly ITestRepository _testRepository;

        public AnswerService(IAnswerRepository answerRepository,ITestRepository testRepository)
        {
            _answerRepository = answerRepository;
            _testRepository = testRepository;
        }
        public void CreateAnswer(List<string> answerIdList, List<string> checkedList, IFormFile fileUp, string[] descriptiveAnswer, int studentId, int testId)
        {
            //Add Test Answers
            for (int i = 0; i < answerIdList.Count; i++)
            {
                AnswerModel model = new AnswerModel();
                model.StudentId = studentId;
                model.TestId = testId;
                model.AnswerNumber = i+1;
                model.AnswerChecked = checkedList[i];
                _answerRepository.CreateAnswer(model);
            }
            //Add Descriptive Answers
            for (int j=0;j<descriptiveAnswer.Length;j++)
            {
                AnswerModel model = new AnswerModel();
                model.StudentId = studentId;
                model.TestId = testId;
                model.AnswerContext = descriptiveAnswer[j];
                if (fileUp != null)
                {
                    model.AnswerFile = FileConvertor.SaveFile(fileUp);
                }
                model.AnswerNumber = (j + 1) + answerIdList.Count;
                model.IsDescriptive = true;
                _answerRepository.CreateAnswer(model);
            }
            var studentTest = _testRepository.GetTestStudentByIds(studentId, testId).Result;
            studentTest.IsSubmitAnswer = true;
            _testRepository.UpdateTestStudents(studentTest);
        }

        public Tuple<List<AnswerViewModel>, List<AnswerViewModel>, string> GetAnswersForStudent(int studentId, int testId)
        {
            var answersList = _answerRepository.GetAnswersForStudent(studentId,testId).Result;
            var discriptiveAnswerList = _answerRepository.GetDescriptiveAnswersForStudent(studentId, testId).Result;
            var answerFile = _answerRepository.GetAnswerFile(studentId,testId);
            List<AnswerViewModel> model = new List<AnswerViewModel>();
            List<AnswerViewModel> descriptivemodel = new List<AnswerViewModel>();
            foreach (var answer in answersList)
            {
                model.Add(new AnswerViewModel()
                {
                    StudentId = answer.StudentId,
                    TestId = answer.TestId,
                    AnswerId = answer.AnswerId,
                    AnswerContext = answer.AnswerContext,
                    AnswerChecked = answer.AnswerChecked,
                    AnswerNumber = answer.AnswerNumber,
                    AnswerFile = answer.AnswerFile
                });
            }
            foreach (var answer in discriptiveAnswerList)
            {
                descriptivemodel.Add(new AnswerViewModel()
                {
                    StudentId = answer.StudentId,
                    TestId = answer.TestId,
                    AnswerId = answer.AnswerId,
                    AnswerContext = answer.AnswerContext,
                    AnswerChecked = answer.AnswerChecked,
                    AnswerNumber = answer.AnswerNumber,
                    AnswerFile = answer.AnswerFile
                });
            }
            return Tuple.Create(model,descriptivemodel, answerFile);
        }
    }
}