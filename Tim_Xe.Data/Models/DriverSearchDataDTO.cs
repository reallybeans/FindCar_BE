using System.Collections.Generic;

namespace Tim_Xe.Data.Models
{
    public class DriverSearchDataDTO
    {
        public string Message { get; set; }
        public IEnumerable<DriverDTO> Data { get; set; }
        public string Status { get; set; }

        public DriverSearchDataDTO(string message, IEnumerable<DriverDTO> data, string status)
        {
            Message = message;
            Data = data;
            Status = status;
        }
    }
}
