namespace Tim_Xe.Data.Models
{
    public class ManagerUpdateDataDTO
    {
        public string Message { get; set; }
        public ManagerUpdateDTO Data { get; set; }
        public string Status { get; set; }

        public ManagerUpdateDataDTO(string message, ManagerUpdateDTO data, string status)
        {
            Message = message;
            Data = data;
            Status = status;
        }
    }
}
