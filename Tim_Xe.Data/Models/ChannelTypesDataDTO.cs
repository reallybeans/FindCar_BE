using System;
using System.Collections.Generic;
using System.Text;

namespace Tim_Xe.Data.Models
{
    public class ChannelTypesDataDTO
    {
        public String Message { get; set; }
        public ChannelTypeDTO Data { get; set; }
        public String Status { get; set; }

        public ChannelTypesDataDTO(string message, ChannelTypeDTO data, string status)
        {
            Message = message;
            Data = data;
            Status = status;
        }
    }
}
