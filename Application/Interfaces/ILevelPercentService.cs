using Application.ViewModels.LevelPercentViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ILevelPercentService
    {
        void CreateLevelPercent(AddLevelPercentViewModel model);
        void UpdateLevelPercent(EditLevelPercentViewModel model);
        void UpdateAllParticipantsInfo(int testId);
    }
}
