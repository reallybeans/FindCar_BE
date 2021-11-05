using System;
using System.Collections.Generic;
using System.Text;

namespace Tim_Xe.Data.Models
{
    public class ManagerCreateDataDTO
    {
        public string Message { get; set; }
        public ManagerCreateDTO Data { get; set; }
        public string Status { get; set; }

        public ManagerCreateDataDTO(string message, ManagerCreateDTO data, string status)
        {
            Message = message;
            Data = data;
            Status = status;
        }
    }
}
