using System.Collections.Generic;

namespace Tim_Xe.Data.Models
{
    public class CustomerSearchDataDTO
    {
        public string Message { get; set; }
        public IEnumerable<CustomerDTO> Data { get; set; }
        public string Status { get; set; }

        public CustomerSearchDataDTO(string message, IEnumerable<CustomerDTO> data, string status)
        {
            Message = message;
            Data = data;
            Status = status;
        }
    }
}
