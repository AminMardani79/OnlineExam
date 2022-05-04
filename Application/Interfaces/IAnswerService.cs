using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.ViewModels.AnswerViewModel;
using Application.ViewModels.TestViewModel;
using Domin.Models;
using Microsoft.AspNetCore.Http;

namespace Application.Interfaces
{
    public interface IAnswerService
    {
        void CreateAnswer(List<string> answerIdList,List<string> checkedList, IFormFile fileUp, string[] descriptiveAnswer, int studentId,int testId);
        Tuple<List<AnswerViewModel>,List<AnswerViewModel>,string> GetAnswersForStudent(int studentId,int testId);
    }
}