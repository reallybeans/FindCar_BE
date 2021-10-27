using System;
using System.Collections.Generic;
using System.Text;

namespace Tim_Xe.Data.Models
{
    public class LoginDriverDataDTO
    {
        public String Message { get; set; }
        public LoginDriverDTO Data { get; set; }
        public String Status { get; set; }

        public LoginDriverDataDTO(string message, LoginDriverDTO data, string status)
        {
            Message = message;
            Data = data;
            Status = status;
        }
    }
}
