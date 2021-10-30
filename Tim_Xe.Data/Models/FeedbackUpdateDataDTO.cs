using System;
using System.Collections.Generic;
using System.Text;

namespace Tim_Xe.Data.Models
{
    public class FeedbackUpdateDataDTO
    {
        public string Message { get; set; }
        public FeedbackUpdateDTO Data { get; set; }
        public string Status { get; set; }

        public FeedbackUpdateDataDTO(string message, FeedbackUpdateDTO data, string status)
        {
            Message = message;
            Data = data;
            Status = status;
        }
    }
}
