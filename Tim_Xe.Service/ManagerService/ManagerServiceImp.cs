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
using Tim_Xe.Service.Shared;
using BCryptNet = BCrypt.Net.BCrypt;

namespace Tim_Xe.Service.ManagerService
{
    public class ManagerServiceImp : IManagerService
    {
        private readonly TimXeDBContext context;
        private readonly ManagerMapping managerMapping;
        public ManagerServiceImp()
        {
            context = new TimXeDBContext();
            managerMapping = new ManagerMapping();
        }

        public async Task<ManagerListDataDTO> GetAllGroupOwnersAsync()
        {
            var result = await context.Managers.Include(m => m.Role).Where(m => m.RoleId == 2).ProjectTo<ManagerDTO>(managerMapping.configManager).ToListAsync();
            if (result.Count() == 0)
            {
                return new ManagerListDataDTO("list is empty", null, "empty");
            }
            else return new ManagerListDataDTO("success", result, "success");
        }
        public async Task<ManagerSearchDataDTO> SearchManagersAsync(ManagerSearchDTO paging)
        {
            try
            {
                if (paging.Pagination.SortOrder == "des")
                {
                    var result = await context.Managers.Include(m => m.Role)
                   .Where(m => m.Name.ToLower().Contains(paging.Name.ToLower())) // like
                   .OrderByDescending(m => m.Id) // search for descending with Softfield id
                   .Skip((int)(paging.Pagination.Page * (paging.Pagination.Size)))
                   .Take((int)paging.Pagination.Size)
                   .ProjectTo<ManagerDTO>(managerMapping.configManager)
                   .ToListAsync();
                    return new ManagerSearchDataDTO("success", result, "success");
                }
                else
                {
                    var result1 = await context.Managers.Include(m => m.Role)
                                   .Where(m => m.Name.ToLower().Contains(paging.Name.ToLower()))
                                   .OrderBy(m => m.Id)
                                   .Skip((int)(paging.Pagination.Page * (paging.Pagination.Size)))
                                   .Take((int)paging.Pagination.Size)
                                   .ProjectTo<ManagerDTO>(managerMapping.configManager)
                                   .ToListAsync();
                    return new ManagerSearchDataDTO("success", result1, "successs");
                }
            }
            catch (Exception e)
            {
                return new ManagerSearchDataDTO("fail", null, "fail");
            }

        }
        public async Task<ManagerDataDTO> GetManagerByIdAsync(int id)
        {
            var result = await context.Managers.Include(m => m.Role).ProjectTo<ManagerDTO>(managerMapping.configManager).FirstOrDefaultAsync(m => m.Id == id);
            if (result == null)
            {
                return new ManagerDataDTO("fail", null, "not available");
            }
            else
            {
                return new ManagerDataDTO("success", result, result.Status);
            }

        }
        public async Task<ManagerCreateDataDTO> CreateManager(ManagerCreateDTO manager)
        {
            var pwd = BCryptNet.HashPassword(manager.Password); // hash password
            manager.Role = manager.Role == "Group Owner" ? "2" : "1";
            var validEmail = ValidateEmail.CheckEmail(manager.Email);
            var validPhone = ValiDatePhone.CheckPhone(manager.Phone);
            if (!validPhone) return new ManagerCreateDataDTO("Phone number is exist", null, "fail");
            else if (!validEmail) return new ManagerCreateDataDTO("email is exist", null, "fail");
            else
            {
                context.Managers.Add(new Manager()
                {
                    Name = manager.Name,
                    Phone = manager.Phone,
                    Password = pwd,
                    Email = manager.Email,
                    RoleId = Int32.Parse(manager.Role),
                    Status = manager.Status,
                    CardId = manager.CardId,
                    CreateAt = DateTime.Now,
                    IsDeleted = false,
                    Img = manager.Img
                });
                await context.SaveChangesAsync();
                return new ManagerCreateDataDTO("create success", manager, "success");
            }
        }
        public async Task<ManagerUpdateDataDTO> UpdateManager(ManagerUpdateDTO manager)
        {
            var pwd = BCryptNet.HashPassword(manager.Password); // hash password
            manager.Role = manager.Role == "Group Owner" ? "2" : "1";
            var existingAccount = await context.Managers.FirstOrDefaultAsync(a => a.Id == manager.Id);
            var validPhone = ValiDatePhone.CheckPhone(manager.Phone);
            if (!validPhone) return new ManagerUpdateDataDTO("Phone number is exist", null, "fail");
            else if (existingAccount != null)
            {
                existingAccount.Name = manager.Name;
                existingAccount.Password = pwd;
                existingAccount.Phone = manager.Phone;
                existingAccount.RoleId = Int32.Parse(manager.Role);
                existingAccount.Status = manager.Status;
                existingAccount.Img = manager.Img;
                context.Managers.Update(existingAccount);
                await context.SaveChangesAsync();
                return new ManagerUpdateDataDTO("update success", manager, "success");
            }
            else
            {
                return new ManagerUpdateDataDTO("update fail", null, "fail");
            }
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

        public async Task<ManagerListDataDTO> GetAllManagersAsync()
        {
            var result = await context.Managers.ProjectTo<ManagerDTO>(managerMapping.configManager).ToListAsync();
            if (result.Count() == 0)
            {
                return new ManagerListDataDTO("list is empty", null, "empty");
            }
            else return new ManagerListDataDTO("success", result, "success");
        }
    }
}
