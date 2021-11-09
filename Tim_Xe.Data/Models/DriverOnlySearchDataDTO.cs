using System.Collections.Generic;

namespace Tim_Xe.Data.Models
{
    public class DriverOnlySearchDataDTO
    {
        public string Message { get; set; }
        public IEnumerable<DriverOnlyDTO> Data { get; set; }
        public string Status { get; set; }

        public DriverOnlySearchDataDTO(string message, IEnumerable<DriverOnlyDTO> data, string status)
        {
            Message = message;
            Data = data;
            Status = status;
        }
    }
}
