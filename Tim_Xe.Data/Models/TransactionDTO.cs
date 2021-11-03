using System;
using System.Collections.Generic;
using System.Text;

namespace Tim_Xe.Data.Models
{
    public class TransactionDTO
    {
        public int Id { get; set; }
        public int? BookingDriverId { get; set; }
        public int? CustomerId { get; set; }
        public int? DriverId { get; set; }
        public string CustomerName { get; set; }
        public bool? Mode { get; set; }
        public double? Price{ get; set; }
        public int? TimeWait { get; set; }
        public string Status { get; set; }
        public DateTime? Date { get; set; }
        public DateTime? StartDate { get; set; }
        public string Description { get; set; }
        public ScheduleDTO Schedule { get; set; }
    }
}
