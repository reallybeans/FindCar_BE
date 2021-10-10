using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Tim_Xe.Data.Repository.Entities
{
    public partial class BookingDriver
    {
        public int IdTrip { get; set; }
        public int IdDriver { get; set; }
        public string Status { get; set; }
        public string Note { get; set; }

        public virtual Driver IdDriverNavigation { get; set; }
        public virtual Booking IdTripNavigation { get; set; }
    }
}
