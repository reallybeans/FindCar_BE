using System.Collections.Generic;

namespace Tim_Xe.Data.Models
{
    public class PriceTimeListDataDTO
    {
        public string Message { get; set; }
        public IEnumerable<PriceTimeDTO> Data { get; set; }
        public string Status { get; set; }

        public PriceTimeListDataDTO(string message, IEnumerable<PriceTimeDTO> data, string status)
        {
            Message = message;
            Data = data;
            Status = status;
        }
    }
}
