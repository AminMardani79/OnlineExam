using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domin.Models
{
    public class OrderModel
    {
        [Key]
        public int OrderId { get; set; }
        [Required]
        public DateTime CreateDate { get; set; }
        public bool IsFinally { get; set; } = false;
        public string OrderCode { set; get; }
        public int StudentId { get; set; }
        [ForeignKey("StudentId")]
        public StudentModel StudentModel { get; set; }
        public IEnumerable<OrderDetailModel> OrderDetailModel { get; set; }
    }
}