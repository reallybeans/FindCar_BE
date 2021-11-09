using System.Collections.Generic;

namespace Tim_Xe.Data.Models
{
    public class AddressDTO
    {
        public string? Origin { get; set; }
        public string? Destination { get; set; }
        public List<string>? Waypoint { get; set; }
    }
}
