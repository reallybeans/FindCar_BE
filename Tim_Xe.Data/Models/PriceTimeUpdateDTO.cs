using System;
using System.Collections.Generic;
using System.Text;

namespace Tim_Xe.Data.Models
{
    public class PriceTimeUpdateDTO
    {
        public int Id { get; set; }
        public int? TimeWait { get; set; }
        public double? Price { get; set; }
        public int? IdVehicleType { get; set; }
        public bool? IsDeleted { get; set; }
    }
}
