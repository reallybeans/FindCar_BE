using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Tim_Xe.Data.Repository.Entities
{
    public partial class Group
    {
        public Group()
        {
            Bookings = new HashSet<Booking>();
            Channels = new HashSet<Channel>();
            Drivers = new HashSet<Driver>();
            Feedbacks = new HashSet<Feedback>();
            News = new HashSet<News>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public int IdManager { get; set; }
        public int? IdCity { get; set; }
        public string Status { get; set; }
        public double PriceCoefficient { get; set; }
        public bool? IsDeleted { get; set; }

        public virtual City IdCityNavigation { get; set; }
        public virtual Manager IdManagerNavigation { get; set; }
        public virtual ICollection<Booking> Bookings { get; set; }
        public virtual ICollection<Channel> Channels { get; set; }
        public virtual ICollection<Driver> Drivers { get; set; }
        public virtual ICollection<Feedback> Feedbacks { get; set; }
        public virtual ICollection<News> News { get; set; }
    }
}
