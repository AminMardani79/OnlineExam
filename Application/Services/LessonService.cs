using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces;
using Application.Others;
using Application.ViewModels.LessonViewModel;
using Application.ViewModels.TeacherViewModel;
using Domin.Interfaces;
using Domin.Models;
using Microsoft.EntityFrameworkCore;

namespace Application.Services
{
    public class LessonService : ILessonService
    {
        private readonly ILessonRepository _lessonRepository;
        private readonly ITestRepository _testRepository;

        public LessonService(ILessonRepository lessonRepository,ITestRepository testRepository)
        {
            _lessonRepository = lessonRepository;
            _testRepository = testRepository;
        }

        public Tuple<List<LessonViewModel>, int, int> GetLessonList(int? id, string search = "", int page = 1)
        {
            var list =  _lessonRepository.GetLessonListByFilter(search).Result;
            List<LessonModel>filter;
            switch (id)
            {
                case null:
                     filter = list.ToList();
                    break;
                default:
                     filter = list.Where(w => w.GradeId == id).ToList();
                    break;
            }
            List<LessonViewModel> models = new List<LessonViewModel>();
            int pageNumber = page;
            int pageCount = PageCounts.PageCount(filter.Count, 6);
            int skip = (page - 1) * 6;
            var lessonList = filter.Skip(skip).Take(6).ToList();
            foreach (var item in lessonList)
            {
                models.Add(new LessonViewModel()
                {
                    LessonId = item.LessonId,
                    LessonName = item.LessonName,
                    GradeName = item.Grade.GradeName
                });
            }
            return Tuple.Create(models, pageCount, pageNumber);
        }

        public async Task<IEnumerable<ItemLessonViewModel>> ItemsLessons()
        {
            var list = await _lessonRepository.GetLessonList();
            List<ItemLessonViewModel>items=new List<ItemLessonViewModel>();
            foreach (var item in list)
            {
                items.Add(new ItemLessonViewModel()
                {
                    LessonId = item.LessonId,
                    LessonName = item.LessonName
                });
            }

            return items;
        }

        public Tuple<List<LessonViewModel>, int, int> GetDeletedLessonList(string search = "", int page = 1)
        {
            var list = _lessonRepository.GetDeletedLessonListByFilter(search).Result.ToList();
            List<LessonViewModel> models = new List<LessonViewModel>();
            int pageNumber = page;
            int pageCount = PageCounts.PageCount(list.Count, 6);
            int skip = (page - 1) * 6;
            var lessonList = list.Skip(skip).Take(6).ToList();
            foreach (var item in lessonList)
            {
                int testsCount = _testRepository.GetTestCountsByLessonId(item.LessonId).Result;
                models.Add(new LessonViewModel()
                {
                    LessonId = item.LessonId,
                    LessonName = item.LessonName,
                    GradeName = item.Grade.GradeName,
                    TestsCount = testsCount
                });
            }
            return Tuple.Create(models, pageCount, pageNumber);
        }
        public async Task<EditLessonViewModel> GetLessonId(int lessonId)
        {
            var lesson = await _lessonRepository.GetLessonId(lessonId);
            EditLessonViewModel edit = new EditLessonViewModel()
            {
                GradeId = lesson.GradeId,
                LessonName = lesson.LessonName,
                LessonId = lesson.LessonId
            };
            return edit;
        }

        public async Task<LessonModel> GetDeletedLessonId(int lessonId)
        {
            var lesson = await _lessonRepository.GetDeletedLessonId(lessonId);
            return lesson;
        }

        public void CreateLesson(AddLessonViewModel model)
        {
            LessonModel lesson = new LessonModel()
            {
                GradeId = model.GradeId,
                LessonName = model.LessonName
            };
            _lessonRepository.CreateLesson(lesson);
        }

        public void UpdateLesson(EditLessonViewModel model)
        {
            var lesson = _lessonRepository.GetLessonId(model.LessonId).Result;
            lesson.GradeId = model.GradeId;
            lesson.LessonName = model.LessonName;
            _lessonRepository.UpdateLesson(lesson);
        }

        public void DeleteLesson(int lessonId)
        {
            var model = _lessonRepository.GetDeletedLessonId(lessonId).Result;
            _lessonRepository.DeleteLesson(model);
        }

        public void AddToTrash(int lessonId)
        {
            var model = _lessonRepository.GetLessonId(lessonId).Result;
            model.IsLessonDelete = true;
            _lessonRepository.UpdateLesson(model);
        }

        public void BackToList(int lessonId)
        {
            var model = _lessonRepository.GetDeletedLessonId(lessonId).Result;
            model.IsLessonDelete = false;
            _lessonRepository.UpdateLesson(model);
        }

        public async Task<IEnumerable<LessonViewModel>> GetLessonsByGradeId(int gradeId)
        {
            var list = await _lessonRepository.GetLessonsByGradeId(gradeId);
            List<LessonViewModel> lessons = new List<LessonViewModel>();
            foreach (var lesson in list)
            {
                lessons.Add(new LessonViewModel()
                {
                    LessonId = lesson.LessonId,
                    LessonName = lesson.LessonName
                });
            }

            return lessons;
        }
    }
}