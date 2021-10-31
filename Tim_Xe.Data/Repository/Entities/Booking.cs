using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Tim_Xe.Data.Repository.Entities
{
    public partial class Booking
    {
        public Booking()
        {
            BookingDrivers = new HashSet<BookingDriver>();
            Feedbacks = new HashSet<Feedback>();
            Locations = new HashSet<Location>();
        }

        public int Id { get; set; }
        public int? IdGroup { get; set; }
        public int? IdCustomer { get; set; }
        public string NameCustomer { get; set; }
        public string PhoneCustomer { get; set; }
        public int? IdVehicleType { get; set; }
        public DateTime? StartAt { get; set; }
        public DateTime? EndAt { get; set; }
        public int? TimeWait { get; set; }
        public double? PriceBooking { get; set; }
        public DateTime? CreateAt { get; set; }
        public int? Status { get; set; }
        public bool? Mode { get; set; }

        public virtual Customer IdCustomerNavigation { get; set; }
        public virtual Group IdGroupNavigation { get; set; }
        public virtual VehicleType IdVehicleTypeNavigation { get; set; }
        public virtual ICollection<BookingDriver> BookingDrivers { get; set; }
        public virtual ICollection<Feedback> Feedbacks { get; set; }
        public virtual ICollection<Location> Locations { get; set; }
    }
}
