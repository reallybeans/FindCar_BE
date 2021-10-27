using System;
using System.Collections.Generic;
using System.Text;

namespace Tim_Xe.Data.Models
{
    public class GroupCreateDataDTO
    {
        public String Message { get; set; }
        public GroupCreateDTO Data { get; set; }
        public String Status { get; set; }

        public GroupCreateDataDTO(string message, GroupCreateDTO data, string status)
        {
            Message = message;
            Data = data;
            Status = status;
        }
    }
}
