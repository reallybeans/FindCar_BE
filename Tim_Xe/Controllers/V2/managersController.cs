using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tim_Xe.Data.Models;
using Tim_Xe.Service.ManagerService;


namespace Tim_Xe.API.Controllers.V2
{
    [Authorize(Roles = "group, admin")]
    [Route("api/v2/managers")]
    [ApiController]
    public class ManagersController : ControllerBase
    {
        private readonly ManagerServiceImp _managerServiceImp;

        public ManagersController()
        {
            _managerServiceImp = new ManagerServiceImp();
        }
        [HttpGet]
        public async Task<ManagerListDataDTO> GetAllManagers()
        {
            return await _managerServiceImp.GetAllManagersAsync();
        }
        [HttpGet("group-owner")]
        public async Task<ManagerListDataDTO> GetAllGroupOwner()
        {
            return await _managerServiceImp.GetAllGroupOwnersAsync();
        }
        [HttpGet("{id}")]
        public async Task<ManagerDataDTO> GetManagerById(int id)
        {
            return await _managerServiceImp.GetManagerByIdAsync(id);
        }
        [HttpPost]
        public async Task<ManagerCreateDataDTO> CreateAsync(ManagerCreateDTO manager)
        {
            return await _managerServiceImp.CreateManager(manager);
        }
        [HttpPut]
        public async Task<ManagerUpdateDataDTO> UpdateAsync(ManagerUpdateDTO manager)
        {
            return await _managerServiceImp.UpdateManager(manager);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<int>> DeleteAsync(int id)
        {
            if (id != 0)
            {
                var status = await _managerServiceImp.DeleteManagerAsync(id);
                if (status)
                    return Ok("Delete Success!");
                else
                    return NotFound("Delete Failed!");
            };
            return NotFound();

        }
        [HttpPost("search")]
        public async Task<ManagerSearchDataDTO> GetManagerById(ManagerSearchDTO managerSearchDTO)
        {
            return await _managerServiceImp.SearchManagersAsync(managerSearchDTO);
        }
        [HttpPost("searchs")]
        public async Task<IEnumerable<ManagerDTO>> Searchs(string search)
        {
            return await _managerServiceImp.Searchs(search);
        }
    }
}





