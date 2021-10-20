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
using BCryptNet = BCrypt.Net.BCrypt;

namespace Tim_Xe.Service.ManagerService
{
    public class ManagerServiceImp
    {
        private readonly TimXeDBContext context;
        private readonly ManagerMapping managerMapping;
        public ManagerServiceImp()
        {
            context = new TimXeDBContext();
            managerMapping = new ManagerMapping();
        }
        public async Task<IEnumerable<ManagerDTO>> GetAllManagersAsync()
        {
            return await context.Managers.Include(m => m.Role).Where(m => m.RoleId == 2).ProjectTo<ManagerDTO>(managerMapping.configManager).ToListAsync();
        }
        public async Task<IEnumerable<ManagerDTO>> SearchManagersAsync(ManagerSearchDTO paging)
        {
            if (paging.Pagination.SortOrder == "des")
            {
                return await context.Managers.Include(m => m.Role)
               .Where(m => m.Email.Contains(paging.Name)) // like
               .OrderByDescending(m => m.Id) // search for descending with Softfield id
               .Skip((int)(paging.Pagination.Page * (paging.Pagination.Size)))
               .Take((int)paging.Pagination.Size)
               .ProjectTo<ManagerDTO>(managerMapping.configManager)
               .ToListAsync();
            }
            else
            {
                return await context.Managers.Include(m => m.Role)
                               .Where(m => m.Email.Contains(paging.Name))
                               .OrderBy(m => m.Id)
                               .Skip((int)(paging.Pagination.Page * (paging.Pagination.Size)))
                               .Take((int)paging.Pagination.Size)
                               .ProjectTo<ManagerDTO>(managerMapping.configManager)
                               .ToListAsync();
            }
           
        }
        public async Task<ManagerDTO> GetManagerByIdAsync(int id)
        {
            var result = await context.Managers.Include(m => m.Role).ProjectTo<ManagerDTO>(managerMapping.configManager).FirstOrDefaultAsync(m => m.Id == id);
            return result;

        }
        public async Task<int> CreateManager(ManagerCreateDTO manager)
        {
            manager.Role = manager.Role == "Group Owner" ? "2" : "1";
            context.Managers.Add(new Manager()
            {
                Name = manager.Name,
                Phone = manager.Phone,
                Password = BCryptNet.HashPassword(manager.Password),
                Email = manager.Email,
                RoleId = Int32.Parse(manager.Role),
                Status = manager.Status,
                CardId = manager.CardId,
                CreateAt = DateTime.Now,
                IsDeleted = false,
                Img = manager.Img
            }); 

            return await context.SaveChangesAsync();
        }
        public async Task<int> UpdateManager(ManagerUpdateDTO manager)
        {
            manager.Role = manager.Role == "Group Owner" ? "2" : "1";
            var existingAccount = await context.Managers.FirstOrDefaultAsync(a => a.Id == manager.Id);
            if (existingAccount != null)
            {
                existingAccount.Name = manager.Name;
                existingAccount.Password = manager.Password;
                existingAccount.Phone = manager.Phone;
                existingAccount.RoleId = Int32.Parse(manager.Role);
                existingAccount.Status = manager.Status;
                existingAccount.Img = manager.Img;
            }
            else
            {
                return 0;
            }

            return await context.SaveChangesAsync();
        }
        public async Task<bool> DeleteManagerAsync(int id)
        {
            var existingAccount = await context.Managers.FirstOrDefaultAsync(m => m.Id == id);

            if (existingAccount != null)
            {
                existingAccount.IsDeleted = true;
                await context.SaveChangesAsync();

                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
