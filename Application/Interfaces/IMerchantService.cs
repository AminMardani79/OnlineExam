using Application.ViewModels.MerchantViewModel;
using Domin.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IMerchantService
    {
        void UpdateMerchant(EditMerchantViewModel model);
        Task<EditMerchantViewModel> GetFirshMerchant();
        
    }
}
