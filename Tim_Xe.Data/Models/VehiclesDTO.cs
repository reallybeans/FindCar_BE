using System;
using System.Collections.Generic;
using System.Text;

namespace Tim_Xe.Data.Models
{
    public class VehiclesDTO
    {
        public int Id { get; set; }
        public string DriverName { get; set; }
        public string VehicleName{ get; set; }
        public string LicensePlate{ get; set; }
        public string VehicleType{ get; set; }
        public string Status{ get; set; }
    }
}
