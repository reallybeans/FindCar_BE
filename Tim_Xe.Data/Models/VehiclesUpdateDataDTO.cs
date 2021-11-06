using System;
using System.Collections.Generic;
using System.Text;

namespace Tim_Xe.Data.Models
{
    public class VehiclesUpdateDataDTO
    {
        public string Message { get; set; }
        public VehiclesUpdateDTO Data { get; set; }
        public string Status { get; set;}

        public VehiclesUpdateDataDTO(string message, VehiclesUpdateDTO data, string status)
        {
            Message = message;
            Data = data;
            Status = status;
        }
    }
}
