using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.OrderViewModel
{
    public class ShowBasketViewModel
    {
        public string TestTitle { get; set; }
        public string TestCode { get; set; }
        public string TestPrice { get; set; }
        public string StartTest { get; set; }
        public string EndTest { get; set; }
        public string TestDayTime { get; set; }
        public int TestId { get; set; }
        public int OrderId { get; set; }
        public int DetailId { get; set; }

    }
}
