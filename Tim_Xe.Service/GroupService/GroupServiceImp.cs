
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
    public class GroupServiceImp
    {
        private readonly TimXeDBContext context;
        private readonly GroupMapping groupMapping;
        public GroupServiceImp()
        {
            context = new TimXeDBContext();
            groupMapping = new GroupMapping();
        }
        public async Task<IEnumerable<GroupDTO>> GetAllGroupsAsync()
        {
        var groupExisted =  await context.Groups.Include(g => g.IdCityNavigation).ToListAsync();
            List<GroupDTO> groupDTO= new List<GroupDTO>();
            foreach(Group x in groupExisted)
            {
                groupDTO.Add(new GroupDTO(x));
            }
            return groupDTO;
        }
        public async Task<GroupDTO> GetGroupByIdAsync(int id)
        {
            var result = await context.Groups.Include(g => g.IdCityNavigation)
                .FirstOrDefaultAsync(g => g.Id == id);
            GroupDTO groupDTO = new GroupDTO(result);
            return groupDTO;

        }
        public async Task<int> CreateGroup(GroupCreateDTO group)
        {
            var city = await context.Cities.FirstOrDefaultAsync(c => c.Name == group.City);
            try {
                context.Groups.Add(new Group()
                {
                    Name = group.Name,
                    Address = group.Address,
                    IdManager = group.IdManager,
                    IdCity = city.Id,
                    Status = group.Status,
                    PriceCoefficient = group.PriceCoefficient,
                    IsDeleted = false
                });
            } catch (Exception e)
            {
                return 0;
            }
            return await context.SaveChangesAsync();
        }
        public async Task<int> UpdateGroup(GroupUpdateDTO group)
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
                    existingGroup.PriceCoefficient = group.PriceCoefficient;
            }
            else
                return 0;
            }
            catch (Exception e)
            {
                return 0;
            }

            return await context.SaveChangesAsync();
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
        public async Task<IEnumerable<GroupDTO>> SearchGroupAsync(GroupSearchDTO paging)
        {
            if (paging.Pagination.SortOrder == "des")
            {
                return await context.Groups
               .Where(m => m.Name.Contains(paging.Name))
               .OrderByDescending(m => m.Id)
               .Skip((int)(paging.Pagination.Page * (paging.Pagination.Size)))
               .Take((int)paging.Pagination.Size)
               .ProjectTo<GroupDTO>(groupMapping.configManager)
               .ToListAsync();
            }
            else
            {
                return await context.Groups
                               .Where(m => m.Name.Contains(paging.Name))
                               .OrderBy(m => m.Id)
                               .Skip((int)(paging.Pagination.Page * (paging.Pagination.Size)))
                               .Take((int)paging.Pagination.Size)
                               .ProjectTo<GroupDTO>(groupMapping.configManager)
                               .ToListAsync();
            }

        }
    }
}
