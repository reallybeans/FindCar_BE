using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Tim_Xe.Data.Models;

namespace Tim_Xe.Service.PriceTimeService
{
    public interface IPriceTimeService
    {
        Task<PriceTimeListDataDTO> GetAllPriceTimesAsync();
        Task<PriceTimeDataDTO> GetPriceTimeByIdAsync(int id);
        Task<PriceTimeCreateDataDTO> CreatePriceTime(PriceTimeCreateDTO priceTime);
        Task<PriceTimeUpdateDataDTO> UpdatePriceTime(PriceTimeUpdateDTO priceTime);
        Task<bool> DeletePriceTimeAsync(int id);
    }
}
