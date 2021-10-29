using System;
using System.Collections.Generic;
using System.Text;

namespace Tim_Xe.Data.Models
{
    public class VehicleTypeListDataDTO
    {
        public string Message { get; set; }
        public IEnumerable<VehicleTypeDTO> Data { get; set; }
        public string Status { get; set; }

        public VehicleTypeListDataDTO(string message, IEnumerable<VehicleTypeDTO> data, string status)
        {
            Message = message;
            Data = data;
            Status = status;
        }
    }
}
