using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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
        public async Task<CustomerDTO> GetCustomerByIdAsync(int id)
        {
            var result = await context.Customers.ProjectTo<CustomerDTO>(customerMapping.configCustomer).FirstOrDefaultAsync(c => c.Id == id);
            return result;
        }
        public async Task<int> CreateCustomer(CustomerCreateDTO customer)
        {
            try
            {
                context.Customers.Add(new Customer()
                {
                    Name = customer.Name,
                    Phone = customer.Phone,
                    Email = customer.Email,
                    Img = customer.Img,
                    Status = customer.Status,
                    CreateAt = DateTime.Now,
                    IsDeleted = false,
                });
            }
            
            catch(Exception e)
            {
                return 0;
            }
            return await context.SaveChangesAsync();
        }
        public async Task<int> UpdateCustomer(CustomerUpdateDTO customer)
        {
            try
            {
                var existingCustomer = await context.Customers.FirstOrDefaultAsync(c => c.Id == customer.Id);
                if (existingCustomer != null)
                {
                    existingCustomer.Name = customer.Name;
                    existingCustomer.Phone = customer.Phone;
                    existingCustomer.Email = customer.Email;
                    existingCustomer.Img = customer.Img;
                    existingCustomer.Status = customer.Status;
                    existingCustomer.IsDeleted = customer.IsDeleted;
                }
                else
                {
                    return 0;
                }
            }           
            catch(Exception e)
            {
                return 0;
            }
            return await context.SaveChangesAsync();
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
    }
}
