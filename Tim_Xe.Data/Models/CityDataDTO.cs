namespace Tim_Xe.Data.Models
{
    public class CityDataDTO
    {
        public string Message { get; set; }
        public CityDTO Data { get; set; }
        public string Status { get; set; }

        public CityDataDTO(string message, CityDTO data, string status)
        {
            Message = message;
            Data = data;
            Status = status;
        }
    }
}
