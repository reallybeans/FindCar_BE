using System;
using System.Collections.Generic;
using System.Text;

namespace Tim_Xe.Data.Models
{
    public class GroupCreateDataDTO
    {
        public string Message { get; set; }
        public GroupCreateDTO Data { get; set; }
        public string Status { get; set; }

        public GroupCreateDataDTO(string message, GroupCreateDTO data, string status)
        {
            Message = message;
            Data = data;
            Status = status;
        }
    }
}
