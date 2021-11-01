using System;
using System.Collections.Generic;
using System.Text;

namespace Tim_Xe.Data.Models
{
    public class BookingCreateDTO
    {   
        public int? IdCustomer { get; set; }
        public string NameCustomer { get; set; }
        public string PhoneCustomer { get; set; }
        public string City { get; set; }
        public DateTime StartAt { get; set; }
        public int TimeWait { get; set; }
        public int TotalTime { get; set; }
        public bool Mode { get; set; }
        public double PriceBooking { get; set; }
        public string Note { get; set; }
        public string VehicleType { get; set; }
        public ScheduleDTO Schedule { get; set; }
    }
}
 