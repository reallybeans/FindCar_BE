using System.Collections.Generic;

namespace Tim_Xe.Data.Models
{
    public class VehiclesDataDTO
    {
        public string Message { get; set; }
        public IEnumerable<VehiclesDTO> Data { get; set; }
        public string Status { get; set; }

        public VehiclesDataDTO(string message, IEnumerable<VehiclesDTO> data, string status)
        {
            Message = message;
            Data = data;
            Status = status;
        }
    }
}
