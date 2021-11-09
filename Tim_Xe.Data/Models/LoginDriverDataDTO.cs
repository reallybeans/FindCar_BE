namespace Tim_Xe.Data.Models
{
    public class LoginDriverDataDTO
    {
        public string Message { get; set; }
        public LoginDriverDTO Data { get; set; }
        public string Status { get; set; }

        public LoginDriverDataDTO(string message, LoginDriverDTO data, string status)
        {
            Message = message;
            Data = data;
            Status = status;
        }
    }
}
