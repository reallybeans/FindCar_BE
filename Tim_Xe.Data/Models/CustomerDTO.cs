using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Tim_Xe.Data.Models
{
    public class CustomerDTO
    {
        public int Id { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter the name")]
        [StringLength(maximumLength: 50, MinimumLength = 5, ErrorMessage = "Length must be between 5 to 50")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Mobile Number is required.")]
        [RegularExpression(@"^([0-9]{10})$", ErrorMessage = "Invalid Mobile Number.")]
        public string Phone { get; set; }
        [Required(ErrorMessage = "Field can't be empty")]
        [DataType(DataType.EmailAddress, ErrorMessage = "E-mail is not valid")]       
        public string Email { get; set; }
        //public string Password { get; set; }
        public string Img { get; set; }
        public string Status { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime? CreateAt { get; set; }
        public bool? IsDeleted { get; set; }
    }
}
