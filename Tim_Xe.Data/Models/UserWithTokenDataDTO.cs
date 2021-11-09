namespace Tim_Xe.Data.Models
{
    public class UserWithTokenDataDTO
    {
        public string Message { get; set; }
        public UserWithToken Data { get; set; }
        public string Status { get; set; }

        public UserWithTokenDataDTO(string message, UserWithToken data, string status)
        {
            Message = message;
            Data = data;
            Status = status;
        }
    }
}
