namespace Tim_Xe.Data.Models
{
    public class PriceKmUpdateDataDTO
    {
        public string Message { get; set; }
        public PriceKmUpdateDTO Data { get; set; }
        public string Status { get; set; }

        public PriceKmUpdateDataDTO(string message, PriceKmUpdateDTO data, string status)
        {
            Message = message;
            Data = data;
            Status = status;
        }
    }
}
