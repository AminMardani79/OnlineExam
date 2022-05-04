using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.OrderViewModel
{
    public class OrderViewModel
    {
        public int OrderId { get; set; }
        public DateTime CreateDate { get; set; }
        public bool IsFinally { get; set; }
        public string OrderCode { set; get; }
        public int StudentId { get; set; }
    }
}
