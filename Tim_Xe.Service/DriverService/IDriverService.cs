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
        Task<IEnumerable<DriverDTO>> SearchDriverAsync(DriverSearchDTO paging);
        Task<DriverDataDTO> GettDriverByIdAsync(int id);
        Task<DriverCreateDataDTO> CreateDriver(DriverCreateDTO driver);
        Task<DriverUpdateDataDTO> UpdateDriver(DriverUpdateDTO driver);
        Task<bool> DeleteDriverAsync(int id);
    }
}
