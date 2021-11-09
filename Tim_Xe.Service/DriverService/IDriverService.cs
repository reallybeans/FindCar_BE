using System.Collections.Generic;
using System.Threading.Tasks;
using Tim_Xe.Data.Models;

namespace Tim_Xe.Service.DriverService
{
    public interface IDriverService
    {
        Task<DriverOnlySearchDataDTO> GetAllDriversAsync();
        Task<DriverListDataDTO> GetAllDriversByIdManagerAsync(int id);
        Task<DriverSearchDataDTO> SearchDriverAsync(DriverSearchDTO paging);
        Task<DriverOnlyDataDTO> GetDriverByIdAsync(int id);
        Task<DriverCreateDataDTO> CreateDriver(DriverCreateDTO driver);
        Task<DriverUpdateDataDTO> UpdateDriver(DriverUpdateDTO driver);
        Task<bool> DeleteDriverAsync(int id);
        Task<DriverUpdateAddressDataDTO> UpdateAddress(DriverUpdateAddressDTO driverUpdateAddressDTO);
        Task<DriverOnlySearchDataDTO> SearchByName(string name);
        Task<DriverOnlySearchDataDTO> SearchByPhone(string phone);
        Task<IEnumerable<DriverOnlyDTO>> SearchDrivers(string search);
    }
}
