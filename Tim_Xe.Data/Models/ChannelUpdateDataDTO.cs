using System;
using System.Collections.Generic;
using System.Text;

namespace Tim_Xe.Data.Models
{
    public class ChannelUpdateDataDTO
    {
        public String Message { get; set; }
        public ChannelUpdateDTO Data { get; set; }
        public  String Status { get; set; }

        public ChannelUpdateDataDTO(string message, ChannelUpdateDTO data, string status)
        {
            Message = message;
            Data = data;
            Status = status;
        }
    }
}
