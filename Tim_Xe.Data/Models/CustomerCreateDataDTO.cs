namespace Tim_Xe.Data.Models
{
    public class CustomerCreateDataDTO
    {
        public string Message { get; set; }
        public CustomerCreateDTO Data { get; set; }
        public string Status { get; set; }

        public CustomerCreateDataDTO(string message, CustomerCreateDTO data, string status)
        {
            Message = message;
            Data = data;
            Status = status;
        }
    }
}
