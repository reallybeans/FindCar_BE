namespace Tim_Xe.Data.Models
{
    public class ChannelTypesDataDTO
    {
        public string Message { get; set; }
        public ChannelTypeDTO Data { get; set; }
        public string Status { get; set; }

        public ChannelTypesDataDTO(string message, ChannelTypeDTO data, string status)
        {
            Message = message;
            Data = data;
            Status = status;
        }
    }
}
