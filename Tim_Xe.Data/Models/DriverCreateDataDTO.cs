using System;
using System.Collections.Generic;
using System.Text;

namespace Tim_Xe.Data.Models
{
    public class DriverCreateDataDTO
    {
        public string Message { get; set; }
        public DriverCreateDTO Data { get; set; }
        public string Status { get; set; }

        public DriverCreateDataDTO(string message, DriverCreateDTO data, string status)
        {
            Message = message;
            Data = data;
            Status = status;
        }
    }
}
