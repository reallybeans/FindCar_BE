namespace Tim_Xe.Data.Models
{
    public class PriceKmCreateDataDTO
    {
        public string Message { get; set; }
        public PriceKmCreateDTO Data { get; set; }
        public string Status { get; set; }

        public PriceKmCreateDataDTO(string message, PriceKmCreateDTO data, string status)
        {
            Message = message;
            Data = data;
            Status = status;
        }
    }
}
