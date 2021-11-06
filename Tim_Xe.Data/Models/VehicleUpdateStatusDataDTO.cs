using System;
using System.Collections.Generic;
using System.Text;

namespace Tim_Xe.Data.Models
{
    public class VehicleUpdateStatusDataDTO
    {
        public string Message { get; set; }
        public VehicleUpdateStatusDTO Data { get; set; }
        public string Status { get; set; }

        public VehicleUpdateStatusDataDTO(string message, VehicleUpdateStatusDTO data, string status)
        {
            Message = message;
            Data = data;
            Status = status;
        }
    }
}
