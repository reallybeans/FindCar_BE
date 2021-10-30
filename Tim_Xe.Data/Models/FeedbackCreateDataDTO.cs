using System;
using System.Collections.Generic;
using System.Text;

namespace Tim_Xe.Data.Models
{
    public class FeedbackCreateDataDTO
    {
        public string Message { get; set; }
        public FeedbackCreateDTO Data { get; set; }
        public string Status { get; set; }

        public FeedbackCreateDataDTO(string message, FeedbackCreateDTO data, string status)
        {
            Message = message;
            Data = data;
            Status = status;
        }
    }
}
