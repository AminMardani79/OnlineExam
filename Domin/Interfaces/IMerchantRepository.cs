using Domin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domin.Interfaces
{
    public interface IMerchantRepository
    {
        void UpdateMerchant(MerchantModel merchant);
        Task<MerchantModel> GetFirstMerchant();
        void Save();
    }
}