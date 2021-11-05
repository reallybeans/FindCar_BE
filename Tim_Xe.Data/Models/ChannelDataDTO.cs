using System;
using System.Collections.Generic;
using System.Text;

namespace Tim_Xe.Data.Models
{
    public class ChannelDataDTO
    {
        public string Message { get; set; }
        public ChannelDTO Data { get; set; }
        public string Status { get; set; }

        public ChannelDataDTO(string message, ChannelDTO data, string status)
        {
            Message = message;
            Data = data;
            Status = status;
        }
    }
}
