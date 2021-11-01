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

namespace Tim_Xe.Service.CustomerService
{
     public class CustomerServiceImp : ICustomerService
    {
        private readonly TimXeDBContext context;
        private readonly CustomerMapping customerMapping;
        public CustomerServiceImp()
        {
            context = new TimXeDBContext();
            customerMapping = new CustomerMapping();
        }
        public async Task<CustomerListDataDTO> GetAllCustomersAsync()
        {
            var result = await context.Customers.Where(c => c.IsDeleted == false).ProjectTo<CustomerDTO>(customerMapping.configCustomer).ToListAsync();
            if (result.Count() == 0)
            {
                return new CustomerListDataDTO("list is empty", null, "empty");
            }
            else return new CustomerListDataDTO("success", result, "success");
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
                var pwd = BCryptNet.HashPassword(customer.Password); // hash password
                var validEmail = ValidateEmail.CheckEmail(customer.Email);
                var validPhone = ValiDatePhone.CheckPhone(customer.Phone);
                if(!validPhone) return new CustomerCreateDataDTO("Phone number is exist", null, "fail");
                else if (!validEmail) return new CustomerCreateDataDTO("email is exist", null, "fail");
                else
                {
                    context.Customers.Add(new Customer()
                    {
                        Name = customer.Name,
                        Phone = customer.Phone,
                        Email = customer.Email,
                        Password = pwd,
                        Img = customer.Img,
                        Status = customer.Status,
                        CreateAt = DateTime.Now,
                        IsDeleted = false,
                    }); ;
                    await context.SaveChangesAsync();
                    return new CustomerCreateDataDTO("create success", customer, "success");
                }               
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
                var pwd = BCryptNet.HashPassword(customer.Password); // hash password
                var existingCustomer = await context.Customers.FirstOrDefaultAsync(c => c.Id == customer.Id);
                if (!customer.Email.Contains(existingCustomer.Email))
                {
                    var validEmail = ValidateEmail.CheckEmail(customer.Email);
                    if (!validEmail) return new CustomerUpdateDataDTO("email is exist", null, "fail");
                }
                if (!customer.Phone.Contains(existingCustomer.Phone))
                {
                    var validPhone = ValiDatePhone.CheckPhone(customer.Phone);
                    if (!validPhone) return new CustomerUpdateDataDTO("Phone number is exist", null, "fail");
                }            
                if (existingCustomer != null)
                {
                    existingCustomer.Name = customer.Name;
                    existingCustomer.Phone = customer.Phone;
                    existingCustomer.Password = pwd;
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
        public async Task<CustomerSearchDataDTO> SearchCustomerAsync(CustomerSearchDTO paging)
        {
            try
            {
                if (paging.Pagination.SortOrder == "des")
                {
                    var result = await context.Customers
                   .Where(m => m.Name.ToLower().Contains(paging.Name.ToLower()))
                   .OrderByDescending(m => m.Id)
                   .Skip((int)(paging.Pagination.Page * (paging.Pagination.Size)))
                   .Take((int)paging.Pagination.Size)
                   .ProjectTo<CustomerDTO>(customerMapping.configCustomer)
                   .ToListAsync();
                    return new CustomerSearchDataDTO("success", result, "success");
                }
                else
                {
                    var result1 = await context.Customers
                                   .Where(m => m.Name.ToLower().Contains(paging.Name.ToLower()))
                                   .OrderBy(m => m.Id)
                                   .Skip((int)(paging.Pagination.Page * (paging.Pagination.Size)))
                                   .Take((int)paging.Pagination.Size)
                                   .ProjectTo<CustomerDTO>(customerMapping.configCustomer)
                                   .ToListAsync();
                    return new CustomerSearchDataDTO("success", result1, "success");
                }
            }
            catch
            {
                return new CustomerSearchDataDTO("fail", null, "fail");
            }

        }
    }
}
