using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Tim_Xe.Data.Repository.Entities
{
    public partial class Feedback
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int GroupId { get; set; }
        public double Ratting { get; set; }
        public DateTime PostDate { get; set; }
        public int BookingId { get; set; }
        public bool? IsDelete { get; set; }
        public string Description { get; set; }

        public virtual Booking Booking { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual Group Group { get; set; }
    }
}
