namespace Tim_Xe.Data.Models
{
    public class PriceTimeUpdateDataDTO
    {
        public string Message { get; set; }
        public PriceTimeUpdateDTO Data { get; set; }
        public string Status { get; set; }

        public PriceTimeUpdateDataDTO(string message, PriceTimeUpdateDTO data, string status)
        {
            Message = message;
            Data = data;
            Status = status;
        }
    }
}
