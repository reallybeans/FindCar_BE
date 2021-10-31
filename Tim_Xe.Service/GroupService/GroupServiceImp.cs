
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tim_Xe.Data.Models;
using Tim_Xe.Data.Repository;
using Tim_Xe.Data.Repository.Entities;

namespace Tim_Xe.Service.GroupService
{
    public class GroupServiceImp : IGroupService
    {
        private readonly TimXeDBContext context;
        private readonly GroupMapping groupMapping;
        public GroupServiceImp()
        {
            context = new TimXeDBContext();
            groupMapping = new GroupMapping();
        }
        public async Task<GroupListDataDTO> GetAllGroupsAsync()
        {
        var groupExisted =  await context.Groups.Include(g => g.IdCityNavigation).ToListAsync();
            List<GroupDTO> groupDTO= new List<GroupDTO>();
            foreach(Group x in groupExisted)
            {
                groupDTO.Add(new GroupDTO(x));
            }
            if (groupDTO.Count() == 0)
            {
                return new GroupListDataDTO("list is empty", null, "empty");
            }
            else return new GroupListDataDTO("success", groupDTO, "success");
        }
        public async Task<GroupDataDTO> GetGroupByIdAsync(int id)
        {
            var result = await context.Groups.Include(g => g.IdCityNavigation)
                .FirstOrDefaultAsync(g => g.Id == id);
            if (result == null)
            {
                return new GroupDataDTO("fail", null, "not Available");
            }
            else
            {
                GroupDTO groupDTO = new GroupDTO(result);
                return new GroupDataDTO("success", groupDTO, groupDTO.Status);
            }
        }
        public async Task<GroupCreateDataDTO> CreateGroup(GroupCreateDTO group)
        {
            var city = await context.Cities.FirstOrDefaultAsync(c => c.CityName == group.City);
            if(city == null)
            {
                return new GroupCreateDataDTO("create fail", null, "fail");
            }
            try {
                context.Groups.Add(new Group()
                {
                    Name = group.Name,
                    Address = group.Address,
                    IdManager = group.IdManager,
                    IdCity = city.Id,
                    Status = group.Status,
                    PriceCoefficient = (double)group.PriceCoefficient,
                    IsDeleted = false
                });
                await context.SaveChangesAsync();
                return new GroupCreateDataDTO("create success", group, "success");
            } catch (Exception e)
            {
                return new GroupCreateDataDTO("create fail", null, "fail");
            }
        }
        public async Task<GroupUpdateDataDTO> UpdateGroup(GroupUpdateDTO group)
        {
            try
            {
                var existingGroup = await context.Groups.FirstOrDefaultAsync(g => g.Id == group.Id);
            if (existingGroup != null)
            {
                    existingGroup.Name = group.Name;
                    existingGroup.Address = group.Address;
                    existingGroup.IdManager = group.IdManager;
                    existingGroup.Status = group.Status;
                    existingGroup.PriceCoefficient = (double)group.PriceCoefficient;
                    context.Update(existingGroup);
                    await context.SaveChangesAsync();
                    return new GroupUpdateDataDTO("update success", group, "success");
                }
            else
                    return new GroupUpdateDataDTO("update fail", null, "fail");
            }
            catch (Exception e)
            {
                return new GroupUpdateDataDTO("update fail", null, "fail");
            }
        }
        public async Task<bool> DeleteGroupAsync(int id)
        {
            var existingGroup = await context.Groups.FirstOrDefaultAsync(g => g.Id == id);

            if (existingGroup != null)
            {
                existingGroup.IsDeleted = true;
                await context.SaveChangesAsync();

                return true;
            }
            else
            {
                return false;
            }
        }
        public async Task<GroupSearchDataDTO> SearchGroupAsync(GroupSearchDTO paging)
        {
            try
            {
                if (paging.Pagination.SortOrder.Contains("des"))
                {
                    var result = await context.Groups
                   .Where(m => m.Name.ToLower().Contains(paging.Name.ToLower()))
                   .OrderByDescending(m => m.Id)
                   .Skip((int)(paging.Pagination.Page * (paging.Pagination.Size)))
                   .Take((int)paging.Pagination.Size)
                   .ProjectTo<GroupDTO>(groupMapping.configManager)
                   .ToListAsync();
                    return new GroupSearchDataDTO("success", result, "success");
                }
                else
                {
                    var result1 = await context.Groups
                                   .Where(m => m.Name.ToLower().Contains(paging.Name.ToLower()))
                                   .OrderBy(m => m.Id)
                                   .Skip((int)(paging.Pagination.Page * (paging.Pagination.Size)))
                                   .Take((int)paging.Pagination.Size)
                                   .ProjectTo<GroupDTO>(groupMapping.configManager)
                                   .ToListAsync();
                    return new GroupSearchDataDTO("success", result1, "success");
                }
            }
            catch(Exception e)
            {
                return new GroupSearchDataDTO("fail", null, "fail");
            }

        }
    }
}
