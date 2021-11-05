using System;
using System.Collections.Generic;
using System.Text;

namespace Tim_Xe.Data.Models
{
    public class NewsUpdateDataDTO
    {
        public string Message { get; set; }
        public NewsUpdateDTO Data { get; set; }
        public string Status { get; set; }

        public NewsUpdateDataDTO(string message, NewsUpdateDTO data, string status)
        {
            Message = message;
            Data = data;
            Status = status;
        }
    }
}
