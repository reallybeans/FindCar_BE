namespace Tim_Xe.Data.Models
{
    public class DriverDataDTO
    {
        public string Message { get; set; }
        public DriverDTO Data { get; set; }
        public string Status { get; set; }

        public DriverDataDTO(string message, DriverDTO data, string status)
        {
            Message = message;
            Data = data;
            Status = status;
        }
    }
}
