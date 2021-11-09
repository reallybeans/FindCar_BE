using System;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Tim_Xe.Data.Repository.Entities
{
    public partial class Transaction
    {
        public int Id { get; set; }
        public int? BookingDriverId { get; set; }
        public int? CustomerId { get; set; }
        public int? DriverId { get; set; }
        public string Status { get; set; }
        public DateTime? Date { get; set; }
        public string Description { get; set; }

        public virtual BookingDriver BookingDriver { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual Driver Driver { get; set; }
    }
}
