using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
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
    [Route("api/v1/groups")]
    [ApiController]
    public class GroupsController : ControllerBase
    {
        private readonly GroupServiceImp _groupServiceImp;
        public GroupsController()
        {
            _groupServiceImp = new GroupServiceImp();
        }
        [HttpGet]
        public async Task<GroupListDataDTO> GetAll()
        {
            return await _groupServiceImp.GetAllGroupsAsync();
        }
        [HttpGet("{id}")]
        public async Task<GroupDataDTO> GetGroupById(int id)
        {
            return await _groupServiceImp.GetGroupByIdAsync(id);
        }
        [HttpPost]
        public async Task<GroupCreateDataDTO> CreateAsync(GroupCreateDTO group)
        {
            return await _groupServiceImp.CreateGroup(group);
        }
        [HttpPut]
        public async Task<GroupUpdateDataDTO> UpdateAsync(GroupUpdateDTO group)
        {
            return await _groupServiceImp.UpdateGroup(group);
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
