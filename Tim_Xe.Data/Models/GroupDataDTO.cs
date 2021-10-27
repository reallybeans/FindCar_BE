using System;
using System.Collections.Generic;
using System.Text;

namespace Tim_Xe.Data.Models
{
    public class GroupDataDTO
    {
        public String Message { get; set; }
        public GroupDTO Data { get; set; }
        public String Status { get; set; }

        public GroupDataDTO(string message, GroupDTO data, string status)
        {
            Message = message;
            Data = data;
            Status = status;
        }
    }
}
