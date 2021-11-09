using System;

namespace Tim_Xe.Data.Models
{
    public class TransactionCreateDTO
    {
        public int? BookingDriverId { get; set; }
        public int? CustomerId { get; set; }
        public int? DriverId { get; set; }
        public string Status { get; set; }
        public DateTime? Date { get; set; }
        public string Description { get; set; }
    }
}
