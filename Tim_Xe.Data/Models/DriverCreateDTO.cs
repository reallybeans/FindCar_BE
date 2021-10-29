using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Tim_Xe.Data.Models
{
    public class DriverCreateDTO
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter the name")]
        [StringLength(maximumLength: 50, MinimumLength = 5, ErrorMessage = "Length must be between 5 to 50")]

        public string Name { get; set; }
        [Required(ErrorMessage = "Mobile Number is required.")]
        [RegularExpression(@"^([0-9]{10})$", ErrorMessage = "Invalid Mobile Number.")]

        public string? Phone { get; set; }
        [Required(ErrorMessage = "Field can't be empty")]
        [DataType(DataType.EmailAddress, ErrorMessage = "E-mail is not valid")]
        public string? Email { get; set; }
        public string CardId { get; set; }
        public string Img { get; set; }
        public string Status { get; set; }
        public int? CreateById { get; set; }
        public string NameVehicle { get; set; }
        public string Latlng { get; set; }
        public string Address { get; set; }
        public string LicensePlate { get; set; }
        public string? VehicleType { get; set; }
        public string StatusVehicle { get; set; }
    }
}
