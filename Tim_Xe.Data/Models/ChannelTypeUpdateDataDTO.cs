namespace Tim_Xe.Data.Models
{
    public class ChannelTypeUpdateDataDTO
    {
        public string Messagr { get; set; }
        public ChannelTypeUpdateDTO Data { get; set; }
        public string Status { get; set; }

        public ChannelTypeUpdateDataDTO(string messagr, ChannelTypeUpdateDTO data, string status)
        {
            Messagr = messagr;
            Data = data;
            Status = status;
        }
    }
}
