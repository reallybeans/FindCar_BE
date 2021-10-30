using System;
using System.Collections.Generic;
using System.Text;

namespace Tim_Xe.Data.Models
{
    public class CityDTO
    {
        public int Id { get; set; }
        public string CityName { get; set; }
        public bool? IsDeleted { get; set; }

        public CityDTO()
        {
        }
        public CityDTO(int id, string cityName, bool? isDeleted)
        {
            Id = id;
            CityName = cityName;
            IsDeleted = isDeleted;
        }
    }
}
