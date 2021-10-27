using System;
using System.Collections.Generic;
using System.Text;

namespace Tim_Xe.Data.Models
{
    public class PriceTimeDataDTO
    {
        public String Message { get; set; }
        public PriceTimeDTO  Data{get;set; }
        public String Status { get; set; }

        public PriceTimeDataDTO(string message, PriceTimeDTO data, string status)
        {
            Message = message;
            Data = data;
            Status = status;
        }
    }
}
