using System.Collections.Generic;

namespace Tim_Xe.Data.Models
{
    public class ChannelTypesListDataDTO
    {
        public string Message { get; set; }
        public IEnumerable<ChannelTypeDTO> Data { get; set; }
        public string Status { get; set; }

        public ChannelTypesListDataDTO(string message, IEnumerable<ChannelTypeDTO> data, string status)
        {
            Message = message;
            Data = data;
            Status = status;
        }
    }
}
