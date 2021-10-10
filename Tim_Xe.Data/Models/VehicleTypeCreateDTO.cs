using System;
using System.Collections.Generic;
using System.Text;

namespace Tim_Xe.Data.Models
{
    public class VehicleTypeCreateDTO
    {
        public string NameType { get; set; }
        public string Note { get; set; }
        public int? NumOfSeat { get; set; }
    }
}
