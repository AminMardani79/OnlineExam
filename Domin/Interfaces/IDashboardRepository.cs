using Domin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domin.Interfaces
{
    public interface IDashboardRepository
    {
        Task<IEnumerable<TestModel>> GetActiveTestList(DateTime date,DateTime dtNow);
        Task<IEnumerable<TestModel>> GetActiveTestList(DateTime date,DateTime dtNow,int masterId);
    }
}
