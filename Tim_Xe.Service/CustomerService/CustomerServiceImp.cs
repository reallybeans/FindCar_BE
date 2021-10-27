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

namespace Tim_Xe.Service.CustomerService
{
     public class CustomerServiceImp
    {
        private readonly TimXeDBContext context;
        private readonly CustomerMapping customerMapping;
        public CustomerServiceImp()
        {
            context = new TimXeDBContext();
            customerMapping = new CustomerMapping();
        }
        public async Task<IEnumerable<CustomerDTO>> GetAllCustomersAsync()
        {
            return await context.Customers.ProjectTo<CustomerDTO>(customerMapping.configCustomer).ToListAsync();
        }
        public async Task<CustomerDataDTO> GetCustomerByIdAsync(int id)
        {
            var result = await context.Customers.ProjectTo<CustomerDTO>(customerMapping.configCustomer).FirstOrDefaultAsync(c => c.Id == id);
            if (result == null)
            {
                return new CustomerDataDTO("fail", null, "not available");
            }
            else return new CustomerDataDTO("success", result, result.Status);
        }
        public async Task<CustomerCreateDataDTO> CreateCustomer(CustomerCreateDTO customer)
        {
            try
            {
                context.Customers.Add(new Customer()
                {
                    Name = customer.Name,
                    Phone = customer.Phone,
                    Email = customer.Email,
                    Password = customer.Password,
                    Img = customer.Img,
                    Status = customer.Status,
                    CreateAt = DateTime.Now,
                    IsDeleted = false,
                }); ;
                await context.SaveChangesAsync();
                return new CustomerCreateDataDTO("create success", customer, "success");
            }
            catch (Exception e)
            {
                return new CustomerCreateDataDTO("create fail", null, "fail");
            }
        }
        public async Task<CustomerUpdateDataDTO> UpdateCustomer(CustomerUpdateDTO customer)
        {
            try
            {
                var existingCustomer = await context.Customers.FirstOrDefaultAsync(c => c.Id == customer.Id);
                if (existingCustomer != null)
                {
                    existingCustomer.Name = customer.Name;
                    existingCustomer.Phone = customer.Phone;
                    existingCustomer.Password = customer.Password;
                    existingCustomer.Email = customer.Email;
                    existingCustomer.Img = customer.Img;
                    existingCustomer.Status = customer.Status;
                    existingCustomer.IsDeleted = customer.IsDeleted;
                    context.Customers.Update(existingCustomer);
                    await context.SaveChangesAsync();
                    return new CustomerUpdateDataDTO("update success", customer, "success");
                }
                else
                {
                    return new CustomerUpdateDataDTO("update fail", null,"fail");
                }
            }           
            catch(Exception e)
            {
                return new CustomerUpdateDataDTO("update fail", null, "fail");
            }
        }
        public async Task<bool> DeleteCustomerAsync(int id)
        {
            var existingCustomer = await context.Customers.FirstOrDefaultAsync(c => c.Id == id);

            if (existingCustomer != null)
            {
                existingCustomer.IsDeleted = true;
                await context.SaveChangesAsync();

                return true;
            }
            else
            {
                return false;
            }
        }
        public async Task<IEnumerable<CustomerDTO>> SearchCustomerAsync(CustomerSearchDTO paging)
        {
            if (paging.Pagination.SortOrder == "des")
            {
                return await context.Customers
               .Where(m => m.Name.Contains(paging.Name))
               .OrderByDescending(m => m.Id)
               .Skip((int)(paging.Pagination.Page * (paging.Pagination.Size)))
               .Take((int)paging.Pagination.Size)
               .ProjectTo<CustomerDTO>(customerMapping.configCustomer)
               .ToListAsync();
            }
            else
            {
                return await context.Customers
                               .Where(m => m.Name.Contains(paging.Name))
                               .OrderBy(m => m.Id)
                               .Skip((int)(paging.Pagination.Page * (paging.Pagination.Size)))
                               .Take((int)paging.Pagination.Size)
                               .ProjectTo<CustomerDTO>(customerMapping.configCustomer)
                               .ToListAsync();
            }

        }
    }
}
