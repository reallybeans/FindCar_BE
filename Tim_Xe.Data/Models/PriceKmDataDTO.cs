using System;
using System.Collections.Generic;
using System.Text;

namespace Tim_Xe.Data.Models
{
    public class PriceKmDataDTO
    {
        public String Message { get; set; }
        public PriceKmDTO Data { get; set; }
        public String Status { get; set; }

        public PriceKmDataDTO(string message, PriceKmDTO data, string status)
        {
            Message = message;
            Data = data;
            Status = status;
        }
    }
}
