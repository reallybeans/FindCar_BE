using System;
using System.Collections.Generic;
using System.Text;

namespace Tim_Xe.Data.Models
{
    public class GroupListDataDTO
    {
        public string Message { get; set; }
        public IEnumerable<GroupDTO> Data { get; set; }
        public string Status { get; set; }

        public GroupListDataDTO(string message, IEnumerable<GroupDTO> data, string status)
        {
            Message = message;
            Data = data;
            Status = status;
        }
    }
}
