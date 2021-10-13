using System;
using System.Collections.Generic;
using System.Text;

namespace Tim_Xe.Data.Repository.Entities
{
    public class BookingCreatePriceDTO
    {
        public string VehicleType { get; set; }
        public bool Mode { get; set; }
        public int? TimeWait { get; set; }
        public int Km { get; set; }
        public string origin { get; set; }
    }
}
