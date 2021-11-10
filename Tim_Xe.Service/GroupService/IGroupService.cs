using System.Collections.Generic;
using System.Threading.Tasks;
using Tim_Xe.Data.Models;

namespace Tim_Xe.Service.GroupService
{
    public interface IGroupService
    {
        Task<GroupListDataDTO> GetAllGroupsAsync();
        Task<GroupDataDTO> GetGroupByIdAsync(int id);
        Task<GroupCreateDataDTO> CreateGroup(GroupCreateDTO group);
        Task<GroupUpdateDataDTO> UpdateGroup(GroupUpdateDTO group);
        Task<bool> DeleteGroupAsync(int id);
        Task<GroupSearchDataDTO> SearchGroupAsync(GroupSearchDTO paging);
        Task<GroupListDataDTO> SearchsAsync(string search);
    }
}
