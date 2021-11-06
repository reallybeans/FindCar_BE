using System;
using System.Collections.Generic;
using System.Text;

namespace Tim_Xe.Data.Models
{
    public class VehiclesCreateDataDTO
    {
        public string Message { get; set; }
        public VehicleCreateDTO Data { get; set; }
        public string Status { get; set; }

        public VehiclesCreateDataDTO(string message, VehicleCreateDTO data, string status)
        {
            Message = message;
            Data = data;
            Status = status;
        }
    }
}
