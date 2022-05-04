using Application.ViewModels.GradeViewModel;
using Domin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IGradeService
    {
        Task<IEnumerable<GradeViewModel>> GetAllGrades();
        Task<IEnumerable<GradeViewModel>> GetAllDeletedGrades();
        Task<EditGradeViewModel> GetGradeById(int gradeId);
        void AddGrade(AddGradeViewModel grade);
        void UpdateGrade(EditGradeViewModel grade);
        void DeleteGrade(int gradeId);
        void BackToList(int gradeId);
        void AddToTrash(int gradeId);
    }
}
