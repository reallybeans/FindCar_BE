using System;
using System.Collections.Generic;
using System.Text;

namespace Tim_Xe.Data.Models
{
    public class CustomerUpdateDataDTO
    {
        public string Message { get; set; }
        public CustomerUpdateDTO Data { get; set; }
        public string Status { get; set; }

        public CustomerUpdateDataDTO(string message, CustomerUpdateDTO data, string status)
        {
            Message = message;
            Data = data;
            Status = status;
        }
    }
}
