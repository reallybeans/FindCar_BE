using System;
using System.Collections.Generic;
using System.Text;

namespace Tim_Xe.Data.Models
{
    public class CityCreateDataDTO
    {
        public string Message { get; set; }
        public CityCreateDTO Data { get; set; }
        public string Status { get; set; }

        public CityCreateDataDTO(string message, CityCreateDTO data, string status)
        {
            Message = message;
            Data = data;
            Status = status;
        }
    }
}
