using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Tim_Xe.Data.Models;

namespace Tim_Xe.Service.CityService
{
    public interface ICityService
    {
        Task<CityListDataDTO> GetAllCitiesAsync();
        Task<CityDataDTO> GetCityByIdAsync(int id);
        Task<CityCreateDataDTO> CreateCity(CityCreateDTO cityCreateDTO);
        Task<CityUpdateDataDTO> UpdateCity(CityUpdateDTO cityUpdateDTO);
        Task<bool> DeleteCityAsync(int id);
    }
}
