using Application.ViewModels.AdminDashboardViewModel;
using Application.ViewModels.TestViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IDashboardService
    {
        Tuple<List<TestsViewModel>,AdminDashboardViewModel> GetActiveTestList();
    }
}