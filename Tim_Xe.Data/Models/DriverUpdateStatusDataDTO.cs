using System;
using System.Collections.Generic;
using System.Text;

namespace Tim_Xe.Data.Models
{
    public class DriverUpdateStatusDataDTO
    {
        public string Message { get; set; }
        public DriverUpdateStatusDTO Data { get; set; }
        public string Status { get; set; }

        public DriverUpdateStatusDataDTO(string message, DriverUpdateStatusDTO data, string status)
        {
            Message = message;
            Data = data;
            Status = status;
        }
    }
}
