using System;
using System.Collections.Generic;
using System.Text;
using Tim_Xe.Data.Repository.Entities;

namespace Tim_Xe.Data.Models
{
    public class VehicleDTO
    {
        public int IdVehicle { get; set; }
        public string NameVehicle { get; set; }
        public string LicensePlate { get; set; }
        public int? IdVehicleType { get; set; }
        public string StatusVehicle { get; set; }
    }
}
