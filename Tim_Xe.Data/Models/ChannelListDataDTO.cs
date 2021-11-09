using System.Collections.Generic;

namespace Tim_Xe.Data.Models
{
    public class ChannelListDataDTO
    {
        public string Message { get; set; }
        public IEnumerable<ChannelDTO> Data { get; set; }
        public string Status { get; set; }

        public ChannelListDataDTO(string message, IEnumerable<ChannelDTO> data, string status)
        {
            Message = message;
            Data = data;
            Status = status;
        }
    }
}
