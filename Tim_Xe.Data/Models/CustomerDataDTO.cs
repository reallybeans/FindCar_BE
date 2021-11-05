using System;
using System.Collections.Generic;
using System.Text;

namespace Tim_Xe.Data.Models
{
    public class CustomerDataDTO
    {
        public string Message { get; set; }
        public CustomerDTO Data { get; set; }
        public string Status { get; set; }

        public CustomerDataDTO(string message, CustomerDTO data, string status)
        {
            Message = message;
            Data = data;
            Status = status;
        }
    }
}
