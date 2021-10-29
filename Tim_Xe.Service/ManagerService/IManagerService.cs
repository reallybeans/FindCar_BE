using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Tim_Xe.Data.Models;

namespace Tim_Xe.Service.ManagerService
{
    public interface IManagerService
    {
        Task<ManagerListDataDTO> GetAllManagersAsync();
        Task<IEnumerable<ManagerDTO>> SearchManagersAsync(ManagerSearchDTO paging);
        Task<ManagerDataDTO> GetManagerByIdAsync(int id);
        Task<ManagerCreateDataDTO> CreateManager(ManagerCreateDTO manager);
        Task<ManagerUpdateDataDTO> UpdateManager(ManagerUpdateDTO manager);
        Task<bool> DeleteManagerAsync(int id);
        Task<ManagerListDataDTO> GetAllGroupOwnersAsync();
    }
}
