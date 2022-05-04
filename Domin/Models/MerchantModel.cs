using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domin.Models
{
    public class MerchantModel
    {
        [Key]
        public int MerchantId { get; set; }
        [Required(ErrorMessage ="مرچنت کی را وارد کنید")]
        public string MerchantKey { get; set; } = "";
    }
}