using System.Collections.Generic;

namespace Tim_Xe.Data.Models
{
    public class GroupSearchDataDTO
    {
        public string Message { get; set; }
        public IEnumerable<GroupDTO> Data { get; set; }
        public string Status { get; set; }

        public GroupSearchDataDTO(string message, IEnumerable<GroupDTO> data, string status)
        {
            Message = message;
            Data = data;
            Status = status;
        }
    }
}
