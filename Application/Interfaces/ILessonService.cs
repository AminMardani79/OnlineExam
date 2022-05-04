using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.ViewModels.LessonViewModel;
using Application.ViewModels.TeacherViewModel;
using Domin.Models;

namespace Application.Interfaces
{
    public interface ILessonService
    {
        Tuple<List<LessonViewModel>, int, int> GetLessonList(int? id,string search = "", int page = 1);
        Task<IEnumerable<ItemLessonViewModel>> ItemsLessons();
        Tuple<List<LessonViewModel>, int, int> GetDeletedLessonList(string search = "", int page = 1);
        Task<EditLessonViewModel> GetLessonId(int lessonId);
        Task<LessonModel> GetDeletedLessonId(int lessonId);
        void CreateLesson(AddLessonViewModel model);
        void UpdateLesson(EditLessonViewModel model);
        void DeleteLesson(int lessonId);
        void AddToTrash(int lessonId);
        void BackToList(int lessonId);
        Task<IEnumerable<LessonViewModel>> GetLessonsByGradeId(int gradeId);
    }
}