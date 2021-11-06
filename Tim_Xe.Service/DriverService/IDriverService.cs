using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Tim_Xe.Data.Models;

namespace Tim_Xe.Service.DriverService
{
    public interface IDriverService
    {
        Task<DriverListDataDTO> GetAllDriversAsync();
        Task<DriverListDataDTO> GetAllDriversByIdManagerAsync(int id);
        Task<DriverSearchDataDTO> SearchDriverAsync(DriverSearchDTO paging);
        Task<DriverDataDTO> GetDriverByIdAsync(int id);
        Task<DriverCreateDataDTO> CreateDriver(DriverCreateDTO driver);
        Task<DriverUpdateDataDTO> UpdateDriver(DriverUpdateDTO driver);
        Task<bool> DeleteDriverAsync(int id);
        Task<DriverUpdateAddressDataDTO> UpdateAddress(DriverUpdateAddressDTO driverUpdateAddressDTO);

    }
}
