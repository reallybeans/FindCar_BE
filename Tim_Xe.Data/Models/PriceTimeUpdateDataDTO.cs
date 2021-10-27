using System;
using System.Collections.Generic;
using System.Text;

namespace Tim_Xe.Data.Models
{
    public class PriceTimeUpdateDataDTO
    {
        public String Message { get; set; }
        public PriceTimeUpdateDTO Data { get; set; }
        public String Status { get; set; }

        public PriceTimeUpdateDataDTO(string message, PriceTimeUpdateDTO data, string status)
        {
            Message = message;
            Data = data;
            Status = status;
        }
    }
}
