namespace Tim_Xe.Data.Models
{
    public class PriceTimeDataDTO
    {
        public string Message { get; set; }
        public PriceTimeDTO Data { get; set; }
        public string Status { get; set; }

        public PriceTimeDataDTO(string message, PriceTimeDTO data, string status)
        {
            Message = message;
            Data = data;
            Status = status;
        }
    }
}
