using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Tim_Xe.Data.Models;

namespace Tim_Xe.Service.PriceKmService
{
    public interface IPriceKmService
    {
        Task<PriceKmListDataDTO> GetAllPriceKmsAsync();
        Task<PriceKmDataDTO> GetPriceKmByIdAsync(int id);
        Task<PriceKmCreateDataDTO> CreatePriceKm(PriceKmCreateDTO priceKm);
        Task<PriceKmUpdateDataDTO> UpdatePriceKm(PriceKmUpdateDTO priceKm);
        Task<bool> DeletePriceKmAsync(int id);
    }
}
