using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domin.Models;

namespace Domin.Interfaces
{
    public interface ILessonRepository
    {
        Task<IEnumerable<LessonModel>> GetLessonList();
        Task<IEnumerable<LessonModel>> GetDeletedLessonList();
        Task<IEnumerable<LessonModel>> GetLessonListByFilter(string search);
        Task<IEnumerable<LessonModel>> GetDeletedLessonListByFilter(string search);
        Task<LessonModel> GetLessonId(int lessonId);
        Task<LessonModel> GetDeletedLessonId(int lessonId);
        void CreateLesson(LessonModel model);
        void UpdateLesson(LessonModel model);
        void DeleteLesson(LessonModel model);
        Task<IEnumerable<LessonModel>> GetLessonsByGradeId(int gradeId);
        Task<LessonModel> GetLessonName(int lessonId);
        Task<int> GetLessonIdByLessonName(string lessonName);
    }
}
