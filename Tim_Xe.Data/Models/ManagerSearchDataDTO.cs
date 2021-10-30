using System;
using System.Collections.Generic;
using System.Text;

namespace Tim_Xe.Data.Models
{
    public class ManagerSearchDataDTO
    {
        public string Message { get; set; }
        public IEnumerable<ManagerDTO> Data { get; set; }
        public string Status { get; set; }

        public ManagerSearchDataDTO(string message, IEnumerable<ManagerDTO> data, string status)
        {
            Message = message;
            Data = data;
            Status = status;
        }
    }
}

