// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Tim_Xe.Data.Repository.Entities
{
    public partial class Location
    {
        public int Id { get; set; }
        public string LatLng { get; set; }
        public string Address { get; set; }
        public int? PointTypeValue { get; set; }
        public int? OrderNumber { get; set; }
        public int? IdBooking { get; set; }

        public virtual Booking IdBookingNavigation { get; set; }
    }
}
