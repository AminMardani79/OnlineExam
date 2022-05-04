using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domin.Interfaces;
using Domin.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    public class StudentRepository:IStudentRepository
    {
        private readonly ExamContext _context;

        public StudentRepository(ExamContext context)
        {
            _context = context;
        }
        
        public async Task<IEnumerable<StudentModel>> GetStudentList()
        {
            return await _context.StudentModels.Include(n => n.Grade).ToListAsync();
        }

        public async Task<IEnumerable<StudentModel>> GetFilterStudentList(string search)
        {
            return await _context.StudentModels.Include(n=> n.Grade).Where(n=> EF.Functions.Like(n.StudentName,$"%{search}%")).ToListAsync();
        }
        public async Task<IEnumerable<StudentModel>> GetStudentDeletedList()
        {
            return await _context.StudentModels.Include(n => n.Grade).Where(w=>w.IsStudentDelete==true).IgnoreQueryFilters().ToListAsync();
        }
        public async Task<IEnumerable<StudentModel>> GetFilterStudentDeletedList(string search)
        {
            return await _context.StudentModels.Include(n => n.Grade).Where(w => w.IsStudentDelete == true && EF.Functions.Like(w.StudentName,$"%{search}%")).IgnoreQueryFilters().ToListAsync();
        }
        public async Task<StudentModel> GetStudentById(int studentId)
        {
            return await _context.StudentModels.Include(n=> n.Grade).SingleOrDefaultAsync(n=> n.StudentId == studentId);
        }

        public async Task<StudentModel> GetStudentById(int studentId,string search)
        {
            return await _context.StudentModels.Include(n=> n.Grade).SingleOrDefaultAsync(n=> n.StudentId == studentId && EF.Functions.Like(n.StudentName,$"%{search}%"));
        }
        public async Task<StudentModel> GetDeletedStudentById(int studentId)
        {
            return await _context.StudentModels.IgnoreQueryFilters()
                .SingleOrDefaultAsync(s => s.StudentId == studentId);
        }

        public void Create(StudentModel model)
        {
            _context.StudentModels.Add(model);Save();
        }

        public void Update(StudentModel model)
        {
            _context.Update(model);Save();
        }

        public void Delete(StudentModel model)
        {
            _context.StudentModels.Remove(model);Save();
        }
        public async Task<int> GetStudentsCount()
        {
            return await _context.StudentModels.CountAsync();
        }

        #region Account

        public async Task<StudentModel> GetStudent(string nationalCode, string password)
        {
            return await _context.StudentModels.Include(n=> n.Grade).SingleOrDefaultAsync(s => s.StudentNationalCode == nationalCode
                                                                          && s.StudentPassword == password);
        }
        #endregion

        void Save()
        {
            _context.SaveChanges();
        }
        #region Master Panel

        public async Task<IEnumerable<StudentModel>> GetStudentListByTeacherId(int teacherId)
        {
            return await _context.MasterWithStudent.Include(n => n.Student).ThenInclude(n=> n.Grade)
                .Where(n => n.TeacherId == teacherId && n.Student.ActiveAccount == true)
                .Select(n => n.Student).ToListAsync();
        }

        public async Task<IEnumerable<StudentModel>> GetFilterStudentListByTeacherId(int? gradeId, string studentName, int teacherId)
        {
            if (gradeId != 0)
            {
                return await _context.MasterWithStudent.Include(n => n.Student).ThenInclude(n => n.Grade)
                    .Where(n => n.TeacherId == teacherId && EF.Functions.Like(n.Student.StudentName, $"%{studentName}%")
                                                         && n.Student.GradeId == gradeId && n.Student.ActiveAccount == true)
                    .Select(n => n.Student).ToListAsync();
            }
            else
            {
                return await _context.MasterWithStudent.Include(n => n.Student).ThenInclude(n => n.Grade)
                    .Where(n => n.TeacherId == teacherId && EF.Functions.Like(n.Student.StudentName, $"%{studentName}%")
                                                         && n.Student.ActiveAccount == true)
                    .Select(n => n.Student).ToListAsync();
            }
        }

        public async Task<IEnumerable<StudentModel>> GetStudentDeletedListByTeacherId(int teacherId)
        {
            return await _context.MasterWithStudent.Include(n => n.Student).ThenInclude(n=> n.Grade)
                .Where(n => n.TeacherId == teacherId && n.Student.IsStudentDelete == true && n.Student.ActiveAccount == true)
                .IgnoreQueryFilters().Select(n => n.Student).ToListAsync();
        }

        public async Task<IEnumerable<StudentModel>> GetFilterStudentDeletedListByTeacherId(int? gradeId, string studentName,
            int teacherId)
        {
            if (gradeId != 0)
            {
                return await _context.MasterWithStudent.Include(n => n.Student).ThenInclude(n => n.Grade)
                    .Where(n => n.TeacherId == teacherId && EF.Functions.Like(n.Student.StudentName, $"%{studentName}%")
                                                         && n.Student.GradeId == gradeId && n.Student.ActiveAccount == true && n.Student.IsStudentDelete == true)
                    .IgnoreQueryFilters().Select(n => n.Student).ToListAsync();
            }
            else
            {
                return await _context.MasterWithStudent.Include(n => n.Student).ThenInclude(n => n.Grade)
                    .Where(n => n.TeacherId == teacherId && EF.Functions.Like(n.Student.StudentName, $"%{studentName}%")
                                                         && n.Student.ActiveAccount == true && n.Student.IsStudentDelete == true)
                    .IgnoreQueryFilters().Select(n => n.Student).ToListAsync();
            }
        }

        public async Task<IEnumerable<StudentModel>> GetStudentsByLessonId(int lessonId)
        {
            return await _context.LessonModels.Include(n=> n.Grade).ThenInclude(n=> n.Students).Where(n => n.LessonId == lessonId)
                .SelectMany(n => n.Grade.Students.Where(n=> n.ActiveAccount == true)).ToListAsync();
        }
        public async Task<bool> IsExistMSModel(int teacherId, int studentId)
        {
            return await _context.MasterWithStudent.AnyAsync(n => n.TeacherId == teacherId && n.StudentId == studentId);
        }

        public void CreateStudentItem(MasterWithStudentModel msModel)
        {
            _context.Add(msModel);
            Save();
        }

        public async Task<bool> ExistUser(string code)
        {
            return await _context.StudentModels.AnyAsync(a => a.StudentNationalCode == code);
        }

        #endregion
    }
}