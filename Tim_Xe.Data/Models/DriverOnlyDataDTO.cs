using System;
using System.Collections.Generic;
using System.Text;

namespace Tim_Xe.Data.Models
{
    public class DriverOnlyDataDTO
    {
        public string Message { get; set; }
        public DriverOnlyDTO Data { get; set; }
        public string Status { get; set; }

        public DriverOnlyDataDTO(string message, DriverOnlyDTO data, string status)
        {
            Message = message;
            Data = data;
            Status = status;
        }
    }
}
