using System;
using System.Collections.Generic;
using System.Text;

namespace Tim_Xe.Data.Models
{
    public class DriverUpdateDataDTO
    {
        public String Message { get; set; }
        public DriverUpdateDTO Data { get; set; }
        public String Status { get; set; }

        public DriverUpdateDataDTO(string message, DriverUpdateDTO data, string status)
        {
            Message = message;
            Data = data;
            Status = status;
        }
    }
}
