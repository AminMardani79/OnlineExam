using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.MerchantViewModel
{
    public class EditMerchantViewModel
    {
        public int MerchantId { get; set; }
        [Required(ErrorMessage = "کد مرچنت را وارد کنید")]
        public string MerchantKey { get; set; }
    }
}