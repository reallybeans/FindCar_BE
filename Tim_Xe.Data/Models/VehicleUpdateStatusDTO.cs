using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Tim_Xe.Data.Models
{
    public class VehicleUpdateStatusDTO
    {
        public int Id { get; set; }
        public string VehicleName { get; set; }
        public string LicensePlate { get; set; }
        public string VehicleType { get; set; }
        public string Status { get; set; }

        public VehicleUpdateStatusDTO(int id, string vehicleName, string licensePlate, string vehicleType, string status)
        {
            Id = id;
            VehicleName = vehicleName;
            LicensePlate = licensePlate;
            VehicleType = vehicleType;
            Status = status;
        }
    }
}
