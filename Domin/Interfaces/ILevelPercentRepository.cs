using Domin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domin.Interfaces
{
    public interface ILevelPercentRepository
    {
        void CreateLevelPercent(LevelPercentModel model);
        void UpdateLevelPercent(LevelPercentModel model);
        Task<LevelPercentModel> GetModelById(int id);
        Task<LevelPercentModel> GetLevelPercentByIds(int studentId,int testId);
        Task<List<LevelPercentModel>> GetRanksByDecending(int testId);
        Task<LevelPercentModel> GetHighestAverageLevel(int testId);
        void Save();
    }
}
