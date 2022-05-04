using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.OrderViewModel
{
    public class OrderDetailViewModel
    {
        public int DetailId { get; set; }
        public double Price { get; set; }
        public int OrderId { get; set; }
        public int TestId { get; set; }
    }
}
