using System.Collections.Generic;
using System.Threading.Tasks;
using Tim_Xe.Data.Models;

namespace Tim_Xe.Service.ManagerService
{
    public interface IManagerService
    {
        Task<ManagerListDataDTO> GetAllManagersAsync();
        Task<ManagerSearchDataDTO> SearchManagersAsync(ManagerSearchDTO paging);
        Task<ManagerDataDTO> GetManagerByIdAsync(int id);
        Task<ManagerCreateDataDTO> CreateManager(ManagerCreateDTO manager);
        Task<ManagerUpdateDataDTO> UpdateManager(ManagerUpdateDTO manager);
        Task<bool> DeleteManagerAsync(int id);
        Task<ManagerListDataDTO> GetAllGroupOwnersAsync();
        Task<ManagerListDataDTO> Searchs(string search);
    }
}
