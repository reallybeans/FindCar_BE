using System;
using System.Collections.Generic;
using System.Text;

namespace Tim_Xe.Data.Models
{
    public class BookingDTO
    {
        public int Id { get; set; }
        public string NameCustomer  { get; set; }
        public string PhoneCustomer { get; set; }
        public int? Status { get; set; }
        public DateTime? StartAt { get; set; }
        public int? TimeWait { get; set; }
        public bool? Mode { get; set; }
        public double PriceBooking { get; set; }
        public ScheduleDTO Schedule { get; set; }
    }
}
