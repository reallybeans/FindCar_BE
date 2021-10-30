using System;
using System.Collections.Generic;
using System.Text;

namespace Tim_Xe.Data.Models
{
    public class CityUpdateDataDTO
    {
        public string Message { get; set; }
        public CityUpdateDTO Data { get; set; }
        public string Status { get; set; }

        public CityUpdateDataDTO(string message, CityUpdateDTO data, string status)
        {
            Message = message;
            Data = data;
            Status = status;
        }
    }
}
