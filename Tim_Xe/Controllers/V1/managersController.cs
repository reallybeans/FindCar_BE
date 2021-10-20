using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tim_Xe.Data.Models;
using Tim_Xe.Data.Repository.Entities;
using Tim_Xe.Service.ManagerService;


namespace Tim_Xe.API.Controllers.V1
{
    //[Authorize(Roles = "group, admin")]
    [Route("api/v1/managers")]
    [ApiController]
    [EnableCors("ApiCorsPolicy")]
    public class ManagersController : ControllerBase
    {
        private readonly ManagerServiceImp _managerServiceImp;

        public ManagersController()
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
            var status =  await _managerServiceImp.DeleteManagerAsync(id);
                if(status)
                return Ok("Delete Success!");
                else
                    return NotFound("Delete Failed!");
            };
            return NotFound();

        }
        [HttpPost("search")]
        public async Task<IEnumerable<ManagerDTO>> GetManagerById(ManagerSearchDTO managerSearchDTO)
        {
            return await _managerServiceImp.SearchManagersAsync(managerSearchDTO);
        }

    }
}
        


         
        
