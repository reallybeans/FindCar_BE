using System;
using System.Collections.Generic;
using System.Text;

namespace Tim_Xe.Data.Models
{
    public class DriverDataDTO
    {
        public String Message { get; set; }
        public DriverDTO Data { get; set; }
        public String Status { get; set; }

        public DriverDataDTO(string message, DriverDTO data, string status)
        {
            Message = message;
            Data = data;
            Status = status;
        }
    }
}
