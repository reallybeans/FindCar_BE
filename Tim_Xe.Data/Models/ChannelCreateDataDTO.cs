using System;
using System.Collections.Generic;
using System.Text;

namespace Tim_Xe.Data.Models
{
    public class ChannelCreateDataDTO
    {
        public string  Message { get; set;}
        public ChannelCreateDTO Data { get; set; }
        public string Status { get; set; }

        public ChannelCreateDataDTO(string message, ChannelCreateDTO data, string status)
        {
            Message = message;
            Data = data;
            Status = status;
        }
    }
}
