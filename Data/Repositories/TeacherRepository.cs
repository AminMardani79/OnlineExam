using Domin.Interfaces;
using Domin.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class TeacherRepository : ITeacherRepository
    {
        private readonly ExamContext _context;
        public TeacherRepository(ExamContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TeacherModel>> GetTeachersList(int take, int skip)
        {
            return await _context.TeacherModels.OrderByDescending(n => n.TeacherId).Skip(skip).Take(take).ToListAsync();
        }

        public async Task<IEnumerable<TeacherModel>> GetDeletedTeachersList(int take, int skip)
        {
            return await _context.TeacherModels.Where(t => t.IsTeacherDeleted == true).IgnoreQueryFilters().OrderByDescending(n => n.TeacherId).Skip(skip).Take(take).ToListAsync();
        }

        public async Task<TeacherModel> GetTeacherById(int teacherId)
        {
            return await _context.TeacherModels.SingleOrDefaultAsync(t => t.TeacherId == teacherId);
        }
        public async Task<TeacherModel> GetDeletedTeacherById(int teacherId)
        {
            return await _context.TeacherModels.Where(t => t.IsTeacherDeleted == true).IgnoreQueryFilters().SingleOrDefaultAsync(t => t.TeacherId == teacherId);
        }

        public async Task<IEnumerable<TeacherLessonModel>> GetTeacherLessons(int id)
        {
            return await _context.TeacherLessonModels.Where(w => w.TeacherId == id).ToListAsync();
        }

        public void CreateTeacher(TeacherModel teacher)
        {
            _context.TeacherModels.Add(teacher);
            Save();
        }

        public void CreateTeacherItems(TeacherLessonModel model)
        {
            _context.TeacherLessonModels.Add(model);
            Save();
        }

        public void DeleteTeacher(TeacherModel teacher)
        {
            _context.TeacherModels.Remove(teacher);
            Save();
        }

        public void DeleteTeacherLesson(TeacherLessonModel model)
        {
            _context.TeacherLessonModels.Remove(model);Save();
        }

        public void UpdateTeacher(TeacherModel teacher)
        {
            _context.TeacherModels.Update(teacher);
            Save();
        }
        public async Task<bool> IsExistCode(string code)
        {
            return await _context.TeacherModels.AnyAsync(n => n.NationalCode == code);
        }
        public int TeachersCount()
        {
            return _context.TeacherModels.Count();
        }

        public int DeleteTeachersCount()
        {
            return _context.TeacherModels.Where(t => t.IsTeacherDeleted == true).IgnoreQueryFilters().Count();
        }
        public void Save()
        {
            _context.SaveChanges();
        }

        public async Task<bool> ExistTeacher(string code, string password)
        {
            return await _context.TeacherModels.Where(w=>w.Password==password&&w.NationalCode==code).AnyAsync();
        }

        public async Task<TeacherModel> GetTeacherByLoginInfo(string code, string password)
        {
            return await _context.TeacherModels.Where(w => w.ActiveAccount == true)
                .SingleOrDefaultAsync(s => s.NationalCode == code && s.Password == password);
        }

        public async Task<IEnumerable<TeacherModel>> GetTeachersByGradeId(int gradeId)
        {
            return await _context.TeacherLessonModels.Include(n=> n.LessonModel).Include(n=> n.TeacherModel).Where(n => n.LessonModel.GradeId == gradeId)
                .Select(n => n.TeacherModel).ToListAsync();
        }

        public async Task<bool> IsExistMSModel(int studentId, int teacherId)
        {
            return await _context.MasterWithStudent.AnyAsync(n => n.StudentId == studentId && n.TeacherId == teacherId);
        }

        public async Task<bool> ExistUser(string code)
        {
            return await _context.TeacherModels.AnyAsync(a => a.NationalCode == code);
        }


        public async Task<IEnumerable<MasterWithStudentModel>> GetMasterWithStudent()
        {
            return await _context.MasterWithStudent.ToListAsync();
        }

        public void CreateTeacherItem(List<MasterWithStudentModel> masterStudents)
        {
            _context.AddRange(masterStudents);
            Save();
        }
    }
}