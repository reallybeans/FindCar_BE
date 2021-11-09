using System.ComponentModel.DataAnnotations;

namespace Tim_Xe.Data.Models
{
    public class ChannelDTO
    {
        public int Id { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter the name")]
        [StringLength(maximumLength: 50, MinimumLength = 3, ErrorMessage = "Length must be between 3 to 50")]
        public string Name { get; set; }
        public string Url { get; set; }
        public int? IdChannelType { get; set; }
        public int? IdGroup { get; set; }
        public bool? IsDeleted { get; set; }
        public string Status { get; set; }
    }
}
