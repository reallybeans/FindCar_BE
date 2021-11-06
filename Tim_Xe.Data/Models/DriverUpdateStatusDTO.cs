using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Tim_Xe.Data.Models
{
    public class DriverUpdateStatusDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Status must be lowercase")]
        [RegularExpression(@".*[a-z].*$", ErrorMessage = "Status must be lowercase")]
        public string Status { get; set; }
    }
}
