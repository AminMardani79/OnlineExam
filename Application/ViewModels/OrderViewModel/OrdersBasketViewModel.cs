using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.OrderViewModel
{
    public class OrdersBasketViewModel
    {
        public string StudentName { get; set; }
        public string OrderCode { get; set; }
        public string CreateDate { get; set; }
        public bool IsFinaly { get; set; }
        public int OrderId { get; set; } 
        public List<DetailsBasketViewModel> Details { get; set; }
    }
}