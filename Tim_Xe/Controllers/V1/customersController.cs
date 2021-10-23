using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tim_Xe.Data.Models;
using Tim_Xe.Service.CustomerService;

namespace Tim_Xe.API.Controllers.V1
{
    [Route("api/v1/customers")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly CustomerServiceImp _customerServiceImp;
        public CustomersController()
        {
            _customerServiceImp = new CustomerServiceImp();
        }
        [HttpGet]
        public async Task<IEnumerable<CustomerDTO>> GetAll()
        {
            return await _customerServiceImp.GetAllCustomersAsync();
        }
        [HttpGet("{id}")]
        public async Task<CustomerDTO> GetCustomerById(int id)
        {
            return await _customerServiceImp.GetCustomerByIdAsync(id);
        }
        [HttpPost]
        public async Task<ActionResult<int>> CreateAsync(CustomerCreateDTO customer)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid data!");
            if (await _customerServiceImp.CreateCustomer(customer) == 1)
                return Ok("Create Success!");
            else return BadRequest("Create Failed!");
        }
        [HttpPut]
        public async Task<ActionResult<int>> UpdateAsync(CustomerUpdateDTO customer)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid data!");
            if (await _customerServiceImp.UpdateCustomer(customer) == 1)
                return Ok("Update Success!");
            else return BadRequest("Update Failed!");
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
        public async Task<IEnumerable<CustomerDTO>> GetCustomerPagingAsync(CustomerSearchDTO customerSearchDTO)
        {
            return await _customerServiceImp.SearchCustomerAsync(customerSearchDTO);
        }
    }
}
