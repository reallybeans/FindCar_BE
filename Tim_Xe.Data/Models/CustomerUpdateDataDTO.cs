using System;
using System.Collections.Generic;
using System.Text;

namespace Tim_Xe.Data.Models
{
    public class CustomerUpdateDataDTO
    {
        public String Message { get; set; }
        public CustomerUpdateDTO Data { get; set; }
        public String Status { get; set; }

        public CustomerUpdateDataDTO(string message, CustomerUpdateDTO data, string status)
        {
            Message = message;
            Data = data;
            Status = status;
        }
    }
}
