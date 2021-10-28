using System;
using System.Collections.Generic;
using System.Text;

namespace Tim_Xe.Data.Models
{
    public class DriverUpdateDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string CardId { get; set; }
        public string Img { get; set; }
        public string Status { get; set; }
        public string Address { get; set; }
        public string NameVehicle { get; set; }
        public string LicensePlate { get; set; }
        public string? VehicleType { get; set; }
        public string StatusVehicle { get; set; }
    }
}
