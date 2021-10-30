using System;
using System.Collections.Generic;
using System.Text;

namespace Tim_Xe.Data.Models
{
    public class CityUpdateDTO
    {
        public int Id { get; set; }
        public string CityName { get; set; }
        public bool? IsDeleted { get; set; }
    }
}
