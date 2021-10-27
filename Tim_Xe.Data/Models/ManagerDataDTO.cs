using System;
using System.Collections.Generic;
using System.Text;

namespace Tim_Xe.Data.Models
{
    public class ManagerDataDTO
    {
        public String Message { get; set; }
        public ManagerDTO Data { get; set; }
        public String Status { get; set; }

        public ManagerDataDTO(string message, ManagerDTO data, string status)
        {
            Message = message;
            Data = data;
            Status = status;
        }
    }
}
