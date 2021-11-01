using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Tim_Xe.Data.Models
{
    public class FeedbackUpdateDTO
    {
        public int Id { get; set; }
        public int? CustomerId { get; set; }
        public int? GroupId { get; set; }
        [Required(ErrorMessage = "Rating Number is required.")]
        [RegularExpression(@"^(([0-5]{1}[.])[05]{1})$", ErrorMessage = "Invalid Rating Number.")]
        public int Ratting { get; set; }
        public DateTime PostDate { get; set; }
        public int? BookingId { get; set; }
        public bool? IsDelete { get; set; }
        public string Description { get; set; }
        public int? DriverId { get; set; }
    }
}
