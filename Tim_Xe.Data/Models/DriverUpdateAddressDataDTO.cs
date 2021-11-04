using System;
using System.Collections.Generic;
using System.Text;

namespace Tim_Xe.Data.Models
{
    public class DriverUpdateAddressDataDTO
    {
        public string Message { get; set; }
        public DriverUpdateAddressDTO Data { get; set; }
        public string Status { get; set; }

        public DriverUpdateAddressDataDTO(string message, DriverUpdateAddressDTO data, string status)
        {
            Message = message;
            Data = data;
            Status = status;
        }
    }
}
