using System;
using System.Collections.Generic;
using System.Text;

namespace Tim_Xe.Data.Models
{
    public class ManagerUpdateDataDTO
    {
        public String Message { get; set; }
        public ManagerUpdateDTO Data { get; set; }
        public String Status { get; set; }

        public ManagerUpdateDataDTO(string message, ManagerUpdateDTO data, string status)
        {
            Message = message;
            Data = data;
            Status = status;
        }
    }
}
