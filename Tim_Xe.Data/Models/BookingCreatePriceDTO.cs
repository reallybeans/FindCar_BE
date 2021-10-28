using System;
using System.Collections.Generic;
using System.Text;

namespace Tim_Xe.Data.Models
{
    public class BookingCreatePriceDTO
    {
        public string VehicleType { get; set; }
        public bool Mode { get; set; }
        public int? TimeWait { get; set; }
        public double Km { get; set; }
        public string City { get; set; }
    }
}
