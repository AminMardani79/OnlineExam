using Application.Interfaces;
using Application.ViewModels.GradeViewModel;
using Domin.Interfaces;
using Domin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class GradeService:IGradeService
    {
        private readonly IGradeRepository _gradeRepository;
        private readonly ITestRepository _testRepository;
        public GradeService(IGradeRepository gradeRepository,ITestRepository testRepository)
        {
            _gradeRepository = gradeRepository;
            _testRepository = testRepository;
        }
        //before task in method need async
        public async Task<IEnumerable<GradeViewModel>> GetAllGrades()
        {
            var list = await _gradeRepository.GetAllGrades();
            List<GradeViewModel>models=new List<GradeViewModel>();
            foreach (var item in list)
            {
                models.Add(new GradeViewModel()
                {
                    GradeId = item.GradeId,
                    GradeName = item.GradeName
                });
            }
            return models;
        }
        public async Task<IEnumerable<GradeViewModel>> GetAllDeletedGrades() 
        {
            var list = await _gradeRepository.GetAllDeletedGrades();
            List<GradeViewModel> models = new List<GradeViewModel>();
            foreach (var item in list)
            {
                int testsCount = _testRepository.GetTestCountsByGradeId(item.GradeId).Result;
                models.Add(new GradeViewModel() 
                { 
                    GradeId = item.GradeId,
                    GradeName = item.GradeName,
                    TestsCount = testsCount
                });
            }
            return models;
        }
        public async Task<EditGradeViewModel> GetGradeById(int gradeId)
        {
            var grade = await _gradeRepository.GetGradeById(gradeId);
            EditGradeViewModel model=new EditGradeViewModel();
            model.GradeId = grade.GradeId;
            model.GradeName = grade.GradeName;
            model.IsGradeDelete = grade.IsGradeDelete;
            return model;
        }

        public void AddGrade(AddGradeViewModel grade)
        {
           GradeModel model=new GradeModel()
           {
               GradeName = grade.GradeName
           };
           _gradeRepository.AddGrade(model);
        }

        public void UpdateGrade(EditGradeViewModel grade)
        {
            var model = _gradeRepository.GetGradeById(grade.GradeId).Result;
            model.GradeName = grade.GradeName;
            model.IsGradeDelete = grade.IsGradeDelete;
            _gradeRepository.UpdateGrade(model);
        }

        public void DeleteGrade(int gradeId)
        {
            var model = _gradeRepository.GetDeletedGradeById(gradeId).Result;
            _gradeRepository.DeleteGrade(model);
        }

        public void BackToList(int gradeId)
        {
            var model = _gradeRepository.GetDeletedGradeById(gradeId).Result;
            model.IsGradeDelete = false;
            _gradeRepository.UpdateGrade(model);
        }

        public void AddToTrash(int gradeId)
        {
            var model = _gradeRepository.GetGradeById(gradeId).Result;
            model.IsGradeDelete = true;
            _gradeRepository.UpdateGrade(model);
        }
    }
}
