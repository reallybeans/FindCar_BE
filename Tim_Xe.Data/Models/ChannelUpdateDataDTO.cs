using System;
using System.Collections.Generic;
using System.Text;

namespace Tim_Xe.Data.Models
{
    public class ChannelUpdateDataDTO
    {
        public string Message { get; set; }
        public ChannelUpdateDTO Data { get; set; }
        public string Status { get; set; }

        public ChannelUpdateDataDTO(string message, ChannelUpdateDTO data, string status)
        {
            Message = message;
            Data = data;
            Status = status;
        }
    }
}
