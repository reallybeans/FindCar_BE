namespace Tim_Xe.Data.Models
{
    public class ChannelTypeCreateDataDTO
    {
        public string Message { get; set; }
        public ChannelTypeCreateDTO Data { get; set; }
        public string Status { get; set; }

        public ChannelTypeCreateDataDTO(string message, ChannelTypeCreateDTO data, string status)
        {
            Message = message;
            Data = data;
            Status = status;
        }
    }
}
