using System;
using System.Collections.Generic;
using System.Text;

namespace Tim_Xe.Data.Models
{
    public class CustomerListDataDTO
    {
        public string Message { get; set; }
        public IEnumerable<CustomerDTO> Data { get; set; }
        public string Status { get; set; }

        public CustomerListDataDTO(string message, IEnumerable<CustomerDTO> data, string status)
        {
            Message = message;
            Data = data;
            Status = status;
        }
    }
}
