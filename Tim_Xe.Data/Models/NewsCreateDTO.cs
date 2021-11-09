using System.ComponentModel.DataAnnotations;

namespace Tim_Xe.Data.Models
{
    public class NewsCreateDTO
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter the name")]
        [StringLength(maximumLength: 50, MinimumLength = 5, ErrorMessage = "Length must be between 5 to 50")]
        public string Name { get; set; }
        public string Content { get; set; }
        public int? IdGroup { get; set; }
    }
}
