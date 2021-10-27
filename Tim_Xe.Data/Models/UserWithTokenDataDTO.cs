using System;
using System.Collections.Generic;
using System.Text;

namespace Tim_Xe.Data.Models
{
    public class UserWithTokenDataDTO
    {
        public String Message { get; set; }
        public UserWithToken Data { get; set; }
        public String Status { get; set; }

        public UserWithTokenDataDTO(string message, UserWithToken data, string status)
        {
            Message = message;
            Data = data;
            Status = status;
        }
    }
}
