using System.Collections.Generic;

namespace Tim_Xe.Data.Models
{
    public class NewsListDataDTO
    {
        public string Message { get; set; }
        public IEnumerable<NewsDTO> Data { get; set; }
        public string Status { get; set; }

        public NewsListDataDTO(string message, IEnumerable<NewsDTO> data, string status)
        {
            Message = message;
            Data = data;
            Status = status;
        }
    }
}
