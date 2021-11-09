using System.Collections.Generic;
using System.Threading.Tasks;
using Tim_Xe.Data.Models;

namespace Tim_Xe.Service.CustomerService
{
    public interface ICustomerService
    {
        Task<CustomerListDataDTO> GetAllCustomersAsync();
        Task<CustomerDataDTO> GetCustomerByIdAsync(int id);
        Task<CustomerCreateDataDTO> CreateCustomer(CustomerCreateDTO customer);
        Task<CustomerUpdateDataDTO> UpdateCustomer(CustomerUpdateDTO customer);
        Task<bool> DeleteCustomerAsync(int id);
        Task<CustomerSearchDataDTO> SearchCustomerAsync(CustomerSearchDTO paging);
        Task<IEnumerable<CustomerDTO>> SearchCustomersAsync(string search);
    }
}
