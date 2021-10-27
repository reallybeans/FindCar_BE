using System;
using System.Collections.Generic;
using System.Text;

namespace Tim_Xe.Data.Models
{
    public class ChannelTypeUpdateDataDTO
    {
        public String Messagr { get; set; }
        public ChannelTypeUpdateDTO Data { get; set; }
        public String Status { get; set; }

        public ChannelTypeUpdateDataDTO(string messagr, ChannelTypeUpdateDTO data, string status)
        {
            Messagr = messagr;
            Data = data;
            Status = status;
        }
    }
}
