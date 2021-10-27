using System;
using System.Collections.Generic;
using System.Text;

namespace Tim_Xe.Data.Models
{
    public class PriceKmCreateDataDTO
    {
        public String Message { get; set; }
        public PriceKmCreateDTO Data { get; set; }
        public String Status { get; set; }

        public PriceKmCreateDataDTO(string message, PriceKmCreateDTO data, string status)
        {
            Message = message;
            Data = data;
            Status = status;
        }
    }
}
