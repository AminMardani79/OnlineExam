using Application.Interfaces;
using Application.ViewModels.MerchantViewModel;
using Domin.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class MerchantService:IMerchantService
    {
        private readonly IMerchantRepository _merchantRepository;
        public MerchantService(IMerchantRepository merchantRepository)
        {
            _merchantRepository = merchantRepository;
        }
        public void UpdateMerchant(EditMerchantViewModel model)
        {
            var merchant =  _merchantRepository.GetFirstMerchant().Result;
            merchant.MerchantKey = model.MerchantKey;
            _merchantRepository.UpdateMerchant(merchant);
        }
        public async Task<EditMerchantViewModel> GetFirshMerchant()
        {
            var merchant = await _merchantRepository.GetFirstMerchant();
            EditMerchantViewModel model = new EditMerchantViewModel();
            model.MerchantId = merchant.MerchantId;
            model.MerchantKey = merchant.MerchantKey;
            return model;
        }
    }
}
