using System;
using System.Collections.Generic;
using System.Text;

namespace Tim_Xe.Data.Models
{
    public class PriceKmUpdateDataDTO
    {
        public String Message { get; set; }
        public PriceKmUpdateDTO Data { get; set; }
        public String Status { get; set; }

        public PriceKmUpdateDataDTO(string message, PriceKmUpdateDTO data, string status)
        {
            Message = message;
            Data = data;
            Status = status;
        }
    }
}
