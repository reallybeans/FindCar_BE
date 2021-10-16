using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tim_Xe.Data.Models;
using Tim_Xe.Service.GroupService;

namespace Tim_Xe.API.Controllers.V1
{
//    [Authorize(Roles = "group, admin")]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class groupsController : ControllerBase
    {
        private readonly GroupServiceImp _groupServiceImp;
        public groupsController()
        {
            _groupServiceImp = new GroupServiceImp();
        }
        [HttpGet]
        public async Task<IEnumerable<GroupDTO>> GetAll()
        {
            return await _groupServiceImp.GetAllGroupsAsync();
        }
        [HttpGet("{id}")]
        public async Task<GroupDTO> GetGroupById(int id)
        {
            return await _groupServiceImp.GetGroupByIdAsync(id);
        }
        [HttpPost]
        public async Task<ActionResult<int>> CreateAsync(GroupCreateDTO group)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid data!");
            if (await _groupServiceImp.CreateGroup(group) == 1)
                return Ok("Create Success!");
            else return BadRequest("Create Failed!");
        }
        [HttpPut]
        public async Task<ActionResult<int>> UpdateAsync(GroupUpdateDTO manager)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid data!");
            if (await _groupServiceImp.UpdateGroup(manager) == 1)
                return Ok("Update Success!");
            else return BadRequest("Update Failed!");
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<int>> DeleteAsync(int id)
        {
            if (id != 0)
            {
                var status = await _groupServiceImp.DeleteGroupAsync(id);
                if (status)
                    return Ok("Delete Success!");
                else
                    return NotFound("Delete Failed!");
            };
            return NotFound();

        }
        [HttpPost("search")]
        public async Task<IEnumerable<GroupDTO>> GetGroupPagingAsync(GroupSearchDTO groupSearchDTO)
        {
            return await _groupServiceImp.SearchGroupAsync(groupSearchDTO);
        }
    }
}
