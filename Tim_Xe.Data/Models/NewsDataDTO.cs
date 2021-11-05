using System;
using System.Collections.Generic;
using System.Text;

namespace Tim_Xe.Data.Models
{
    public class NewsDataDTO
    {
        public string Message { get; set; }
        public NewsDTO Data { get; set; }
        public string Status { get; set; }

        public NewsDataDTO(string message, NewsDTO data, string status)
        {
            Message = message;
            Data = data;
            Status = status;
        }
    }
}
