﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Tim_Xe.Data.Models
{
    public class VehiclesUpdateDTO
    {
        public int Id { get; set; }
        public string VehicleName { get; set; }
        public string LicensePlate { get; set; }
        public string VehicleType { get; set; }
        public bool IsDelete { get; set; }
    }
}