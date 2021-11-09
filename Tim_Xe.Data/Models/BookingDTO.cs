using System;

namespace Tim_Xe.Data.Models
{
    public class BookingDTO
    {
        public int Id { get; set; }
        public string NameCustomer { get; set; }
        public string PhoneCustomer { get; set; }
        public int? Status { get; set; }
        public DateTime? StartAt { get; set; }
        public int? TimeWait { get; set; }
        public bool? Mode { get; set; }
        public double PriceBooking { get; set; }
        public DateTime? DateEnd { get; set; }
        public string City { get; set; }
        public string Code { get; set; }
        public int TypeVehicle { get; set; }
        public ScheduleDTO Schedule { get; set; }
    }
}
