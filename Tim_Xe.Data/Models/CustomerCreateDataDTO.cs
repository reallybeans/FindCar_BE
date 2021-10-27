using System;
using System.Collections.Generic;
using System.Text;

namespace Tim_Xe.Data.Models
{
    public  class CustomerCreateDataDTO
    {
        public String Message { get; set; }
        public CustomerCreateDTO Data { get; set; }
        public String Status { get; set; }

        public CustomerCreateDataDTO(string message, CustomerCreateDTO data, string status )
        {
            Message = message;
            Data = data;
            Status = status;
        }
    }
}
