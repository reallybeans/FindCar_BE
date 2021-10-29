using System;
using System.Collections.Generic;
using System.Text;

namespace Tim_Xe.Data.Models
{
    public class PriceKmListDataDTO
    {
        public string Message { get; set; }
        public IEnumerable<PriceKmDTO> Data { get; set; }
        public string Status { get; set; }

        public PriceKmListDataDTO(string message, IEnumerable<PriceKmDTO> data, string status)
        {
            Message = message;
            Data = data;
            Status = status;
        }
    }
}
