using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tim_Xe.Data.Models;
using Tim_Xe.Service.ManagerService;


namespace Tim_Xe.API.Controllers.V1
{
    [Authorize(Roles = "group, admin")]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class managersController : ControllerBase
    {
        private readonly ManagerServiceImp _managerServiceImp;

        public managersController()
        {
            _managerServiceImp = new ManagerServiceImp();
        }
        [HttpGet]
        public async Task<IEnumerable<ManagerDTO>> GetAll()
        {
            return await _managerServiceImp.GetAllManagersAsync();
        }
        [HttpGet("{id}")]
        public async Task<ManagerDTO> GetManagerById(int id)
        {
            return await _managerServiceImp.GetManagerByIdAsync(id);
        }
        [HttpPost]
        public async Task<ActionResult<int>> CreateAsync(ManagerCreateDTO manager)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid data!");
            if (await _managerServiceImp.CreateManager(manager) == 1)
                return Ok("Create Success!");
            else return BadRequest("Create Failed!");
        }
        [HttpPut]
        public async Task<ActionResult<int>> UpdateAsync(ManagerUpdateDTO manager)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid data!");
            if (await _managerServiceImp.UpdateManager(manager) == 1)
                return Ok("Update Success!");
            else return BadRequest("Update Failed!");
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<int>> DeleteAsync(int id)
        {
            if (id != 0)
            {
               await _managerServiceImp.DeleteManagerAsync(id);
                return Ok("Delete Success!");
            };
            return NotFound();

        }
    }
}
        


         
        
