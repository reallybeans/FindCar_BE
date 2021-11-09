namespace Tim_Xe.Data.Models
{
    public class ManagerDataDTO
    {
        public string Message { get; set; }
        public ManagerDTO Data { get; set; }
        public string Status { get; set; }

        public ManagerDataDTO(string message, ManagerDTO data, string status)
        {
            Message = message;
            Data = data;
            Status = status;
        }
    }
}
