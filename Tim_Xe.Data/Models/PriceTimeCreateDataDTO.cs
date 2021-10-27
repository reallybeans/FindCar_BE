using System;
using System.Collections.Generic;
using System.Text;

namespace Tim_Xe.Data.Models
{
    public class PriceTimeCreateDataDTO
    {
        public String Message { get; set; }
        public PriceTimeCreateDTO Data { get; set; }
        public String Status { get; set; }

        public PriceTimeCreateDataDTO(string message, PriceTimeCreateDTO data, string status)
        {
            Message = message;
            Data = data;
            Status = status;
        }
    }
}
