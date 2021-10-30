using System;
using System.Collections.Generic;
using System.Text;

namespace Tim_Xe.Data.Models
{
    public class FeedbackListDataDTO
    {
        public string Message { get; set; }
        public IEnumerable<FeedbackDTO> Data { get; set; }
        public string Status { get; set; }

        public FeedbackListDataDTO(string message, IEnumerable<FeedbackDTO> data, string status)
        {
            Message = message;
            Data = data;
            Status = status;
        }
    }
}
