using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Tim_Xe.Data.Repository.Entities
{
    public partial class PriceKm
    {
        public int Id { get; set; }
        public int? Km { get; set; }
        public double? Price { get; set; }
        public string Description { get; set; }
        public int? IdVehicleType { get; set; }
        public bool? IsDeleted { get; set; }

        public virtual VehicleType IdVehicleTypeNavigation { get; set; }
    }
}
