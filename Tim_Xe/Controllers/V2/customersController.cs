﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tim_Xe.Data.Models;
using Tim_Xe.Service.CustomerService;

namespace Tim_Xe.API.Controllers.V2
{

    [Authorize(Roles = "group, admin, customer")]
    [Route("api/v2/customers")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly CustomerServiceImp _customerServiceImp;
        public CustomersController()
        {
            _customerServiceImp = new CustomerServiceImp();
        }
        [HttpGet]
        public async Task<CustomerListDataDTO> GetAll()
        {
            return await _customerServiceImp.GetAllCustomersAsync();
        }
        [HttpGet("{id}")]
        public async Task<CustomerDataDTO> GetCustomerById(int id)
        {
            return await _customerServiceImp.GetCustomerByIdAsync(id);
        }
        [HttpPost]
        public async Task<CustomerCreateDataDTO> CreateAsync(CustomerCreateDTO customer)
        {
            return await _customerServiceImp.CreateCustomer(customer);
        }
        [HttpPut]
        public async Task<CustomerUpdateDataDTO> UpdateAsync(CustomerUpdateDTO customer)
        {
            return await _customerServiceImp.UpdateCustomer(customer);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<int>> DeleteAsync(int id)
        {
            if (id != 0)
            {
                var resutl = await _customerServiceImp.DeleteCustomerAsync(id);
                if (resutl)
                {
                    return Ok("Delete Success!");
                }
                else return BadRequest("Delete Failed!");
            };
            return NotFound();

        }
        [HttpPost("search")]
        public async Task<CustomerSearchDataDTO> GetCustomerPagingAsync(CustomerSearchDTO customerSearchDTO)
        {
            return await _customerServiceImp.SearchCustomerAsync(customerSearchDTO);
        }
        [HttpPost("searchs")]
        public async Task<CustomerListDataDTO> SearchCustomers(string search)
        {
            return await _customerServiceImp.SearchCustomersAsync(search);
        }
    }
}
