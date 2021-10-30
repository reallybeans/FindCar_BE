using System;
using System.Collections.Generic;
using System.Text;

namespace Tim_Xe.Data.Models
{
    public class FeedbackDataDTO
    {
        public string Message { get; set; }
        public FeedbackDTO Data { get; set; }
        public string Status { get; set; }

        public FeedbackDataDTO(string message, FeedbackDTO data, string status)
        {
            Message = message;
            Data = data;
            Status = status;
        }
    }
}
