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

        public VehiclesDTO(int id, string driverName, string vehicleName, string licensePlate, string vehicleType, string status)
        {
            Id = id;
            DriverName = driverName;
            VehicleName = vehicleName;
            LicensePlate = licensePlate;
            VehicleType = vehicleType;
            Status = status;
        }

        public VehiclesDTO()
        {
        }
    }
}
