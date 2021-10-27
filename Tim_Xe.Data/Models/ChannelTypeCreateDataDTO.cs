using System;
using System.Collections.Generic;
using System.Text;

namespace Tim_Xe.Data.Models
{
    public class ChannelTypeCreateDataDTO
    {
        public String Message { get; set; }
        public ChannelTypeCreateDTO Data { get; set; }
        public String Status { get; set; }

        public ChannelTypeCreateDataDTO(string message, ChannelTypeCreateDTO data, string status)
        {
            Message = message;
            Data = data;
            Status = status;
        }
    }
}
