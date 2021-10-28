using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Tim_Xe.Data.Repository.Entities
{
    public partial class Driver
    {
        public Driver()
        {
            BookingDrivers = new HashSet<BookingDriver>();
            Feedbacks = new HashSet<Feedback>();
            Vehicles = new HashSet<Vehicle>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string CardId { get; set; }
        public string Img { get; set; }
        public bool? IsDeleted { get; set; }
        public string Status { get; set; }
        public DateTime? CreateAt { get; set; }
        public int? CreateById { get; set; }
        public double? Revenue { get; set; }

        public virtual Manager CreateBy { get; set; }
        public virtual ICollection<BookingDriver> BookingDrivers { get; set; }
        public virtual ICollection<Feedback> Feedbacks { get; set; }
        public virtual ICollection<Vehicle> Vehicles { get; set; }
    }
}
