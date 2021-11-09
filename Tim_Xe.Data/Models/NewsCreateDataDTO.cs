namespace Tim_Xe.Data.Models
{
    public class NewsCreateDataDTO
    {
        public string Message { get; set; }
        public NewsCreateDTO Data { get; set; }
        public string Status { get; set; }

        public NewsCreateDataDTO(string message, NewsCreateDTO data, string status)
        {
            Message = message;
            Data = data;
            Status = status;
        }
    }
}
