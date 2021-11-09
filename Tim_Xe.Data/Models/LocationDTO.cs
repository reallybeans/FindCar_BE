namespace Tim_Xe.Data.Models
{
    public class LocationDTO
    {
        public int Id { get; set; }
        public string LatLng { get; set; }
        public string Address { get; set; }
        public int? PointTypeValue { get; set; }
        public int? OrderNumber { get; set; }
        public int? IdBooking { get; set; }
    }
}
