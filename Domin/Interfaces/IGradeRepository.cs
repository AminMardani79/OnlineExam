using Domin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domin.Interfaces
{
    public interface IGradeRepository
    {
        Task<IEnumerable<GradeModel>> GetAllGrades();
        Task<IEnumerable<GradeModel>> GetAllDeletedGrades();
        Task<GradeModel> GetGradeById(int gradeId);
        Task<GradeModel> GetDeletedGradeById(int gradeId);
        void AddGrade(GradeModel grade);
        void UpdateGrade(GradeModel grade);
        void DeleteGrade(GradeModel model);
        void Save();
    }
}
