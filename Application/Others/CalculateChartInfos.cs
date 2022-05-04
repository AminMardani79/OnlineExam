using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Others
{
    public static class CalculateChartInfos
    {
        public static void CalculatePercent(this ref double participated, ref double notParticipated, ref double finishedStudents, ref double unFinishedStudents)
        {
            if (participated != 0 || notParticipated != 0)
            {
                participated = (participated / (participated + notParticipated)) * 100;
                notParticipated = 100 - participated;
            }
            if (finishedStudents != 0 || unFinishedStudents != 0)
            {
                finishedStudents = (finishedStudents / (finishedStudents + unFinishedStudents)) * 100;
                unFinishedStudents = 100 - finishedStudents;
            }
        }
    }
}
