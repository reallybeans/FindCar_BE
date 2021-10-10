using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Tim_Xe.Data.Repository.Entities
{
    public partial class VehicleType
    {
        public VehicleType()
        {
            Bookings = new HashSet<Booking>();
            PriceKms = new HashSet<PriceKm>();
            PriceTimes = new HashSet<PriceTime>();
            Vehicles = new HashSet<Vehicle>();
        }

        public int Id { get; set; }
        public string NameType { get; set; }
        public string Note { get; set; }
        public int? NumOfSeat { get; set; }

        public virtual ICollection<Booking> Bookings { get; set; }
        public virtual ICollection<PriceKm> PriceKms { get; set; }
        public virtual ICollection<PriceTime> PriceTimes { get; set; }
        public virtual ICollection<Vehicle> Vehicles { get; set; }
    }
}
