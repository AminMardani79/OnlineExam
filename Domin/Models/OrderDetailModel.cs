using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domin.Models
{
    public class OrderDetailModel
    {
        [Key]
        public int DetailId { get; set; }
        [Required]
        public double Price { get; set; }
        // Relations
        public int OrderId { get; set; }
        [ForeignKey("OrderId")]
        public OrderModel Order { get; set; }
        public int TestId { get; set; }
        [ForeignKey("TestId")]
        public TestModel TestModel { get; set; }
    }
}