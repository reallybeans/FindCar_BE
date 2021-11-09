using System.Collections.Generic;

namespace Tim_Xe.Data.Models
{
    public class CityListDataDTO
    {
        public string Message { get; set; }
        public IEnumerable<CityDTO> Data { get; set; }
        public string Status { get; set; }

        public CityListDataDTO(string message, IEnumerable<CityDTO> data, string status)
        {
            Message = message;
            Data = data;
            Status = status;
        }
    }
}
