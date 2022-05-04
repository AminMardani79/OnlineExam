using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Application.Interfaces;
using Application.Others;
using Application.ViewModels.TestViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using PonishaExam.Helper;

namespace PonishaExam.Areas.Student.Controllers
{
    [Area("Student")]
    [StudentAuthorize]
    public class AnswerController : Controller
    {
        private readonly IAnswerService _answerService;

        public AnswerController(IAnswerService answerService)
        {
            _answerService = answerService;
        }
        [HttpPost]
        [Route("/StudentPanel/CreateAnswer")]
        public IActionResult CreateAnswer(IFormCollection iformCollection, IFormFile fileUp, string[] descriptiveAnswer, int studentId, int testId, int lessonId)
        {
            string[] answerIds = iformCollection["answerId"];
            var answerIdList = answerIds.Skip(descriptiveAnswer.Length).ToList();
            string[] checkedValues = new string[] { };
            var checkedList = checkedValues.ToList();
            for (int i = 0; i < answerIds.Length; i++)
            {
                checkedList.Add(iformCollection[$"radio-{i + 1}-checked"]);
            }
            _answerService.CreateAnswer(answerIdList, checkedList, fileUp, descriptiveAnswer, studentId, testId);
            int submitAnswerId = 1;
            return Redirect($"/StudentPanel/Test/{lessonId}/{submitAnswerId}");
        }
        [HttpGet]
        [Route("/StudentPanel/Test/ShowAnswers/{testId}/{lessonId}")]
        public IActionResult ShowAnswers(int testId, int lessonId)
        {
            int studentId = int.Parse(User.GetStudentId());
            ViewBag.LessonId = lessonId;
            var answers = _answerService.GetAnswersForStudent(studentId, testId);
            return View(answers);
        }
        #region DownloadFile

        [HttpGet]
        [Route("/StudentPanel/DownloadFile/{fileName}")]
        public async Task<IActionResult> DownloadFileAsync(string fileName)
        {
            if (fileName == null)
            {
                return null;
            }
            else
            {
                string pathFile = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "files", fileName);
                var memory = new MemoryStream();
                using (var stream = new FileStream(pathFile, FileMode.Open))
                {
                    await stream.CopyToAsync(memory);
                }
                memory.Position = 0;
                return File(memory, DownloadFile.GetContentType(pathFile), Path.GetFileName(pathFile));
            }
        }

        #endregion
    }
}