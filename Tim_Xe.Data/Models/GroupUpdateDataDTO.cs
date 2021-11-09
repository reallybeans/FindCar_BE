using System;

namespace Tim_Xe.Data.Models
{
    public class GroupUpdateDataDTO
    {
        public String Message { get; set; }
        public GroupUpdateDTO Data { get; set; }
        public String Status { get; set; }

        public GroupUpdateDataDTO(string message, GroupUpdateDTO data, string status)
        {
            Message = message;
            Data = data;
            Status = status;
        }
    }
}
