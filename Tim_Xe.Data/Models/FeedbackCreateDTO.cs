
using System;
using System.ComponentModel.DataAnnotations;

namespace Tim_Xe.Data.Models
{
    public class FeedbackCreateDTO
    {
        public int? CustomerId { get; set; }
        [Required(ErrorMessage = "Rating Number is required.")]
        [RegularExpression(@"^([0-5]{1})$", ErrorMessage = "Invalid Rating Number.")]
        public int Ratting { get; set; }
        public DateTime PostDate { get; set; }
        public int? BookingId { get; set; }
        public string Description { get; set; }
        public int? DriverId { get; set; }
    }
}
