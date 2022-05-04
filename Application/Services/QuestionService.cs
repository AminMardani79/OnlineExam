using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces;
using Application.Others;
using Application.ViewModels.QuestionViewModel;
using Domin.Interfaces;
using Domin.Models;

namespace Application.Services
{
    public class QuestionService:IQuestionService
    {
        private readonly IQuestionRepository _questionRepository;
        private readonly ILessonRepository _lessonRepository;

        public QuestionService(IQuestionRepository questionRepository,ILessonRepository lessonRepository)
        {
            _questionRepository = questionRepository;
            _lessonRepository = lessonRepository;
        }
      
        public Tuple<List<QuestionViewModel>, int, int> GetAllQuestions(int id,int page)
        {
            var list = _questionRepository.GetAllQuestion(id).Result.ToList();
            List<QuestionViewModel>questions=new List<QuestionViewModel>();
            int pageNumber = page;
            int pageCount = PageCounts.PageCount(list.Count, 10);
            int skip = (page - 1) * 10;
            var questionList = list.Skip(skip).Take(10).ToList();
            foreach (var item in questionList)
            {
                questions.Add(new QuestionViewModel()
                {
                    Id = item.Id,
                    Score = item.Score,
                    Type = item.Descriptive,
                    QuestionNumber = item.QuestionNumber.ToString(),
                    KeyAnswer = item.TestKeyAnswer,
                    LessonId = item.LessonId
                });
            }
            return Tuple.Create(questions, pageCount, pageNumber);
        }

        public void AddQuestion(CreateQuestionViewModel model)
        {
            QuestionModel question=new QuestionModel();
            question.TestId = model.TestId;
            question.Descriptive = model.Descriptive;
            question.Score = Convert.ToInt32(model.Score);
            question.Question = model.Question;
            question.QuestionNumber = model.QuestionNumber;
            question.TestKeyAnswer = model.TestKeyAnswer;
            question.LessonId = model.LessonId;
            _questionRepository.Create(question);
        }

        public void EditQuestion(EditQuestionViewModel model)
        {
            var question = _questionRepository.GetQuestionById(model.Id).Result;
            question.TestId = model.TestId;
            question.Descriptive = model.Descriptive;
            question.Score = model.Score;
            question.Question = model.Question;
            question.QuestionNumber = model.QuestionNumber;
            question.TestKeyAnswer = model.TestKeyAnswer;
            question.LessonId = model.LessonId;
            _questionRepository.Edit(question);
        }

        public async Task<EditQuestionViewModel> GetQuestionById(int id)
        {
            EditQuestionViewModel question=new EditQuestionViewModel();
            var model = await _questionRepository.GetQuestionById(id);
            question.TestId = model.TestId;
            question.Descriptive = model.Descriptive;
            question.Score = model.Score;
            question.Question = model.Question;
            question.Id = model.Id;
            question.QuestionNumber = model.QuestionNumber;
            question.TestKeyAnswer = model.TestKeyAnswer;
            question.LessonId = model.LessonId;
            return question;
        }

        public void Delete(int id)
        {
            var model = _questionRepository.GetQuestionById(id).Result;
            _questionRepository.Delete(model);
        }
        public async Task<int> GetQuestionScore(string lessonName)
        {
            var lessonId = await _lessonRepository.GetLessonIdByLessonName(lessonName);
            var question = await _questionRepository.GetQuestionScore(lessonId);
            return question.Score;
        }
    }
}
