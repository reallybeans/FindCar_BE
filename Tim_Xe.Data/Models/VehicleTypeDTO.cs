namespace Tim_Xe.Data.Models
{
    public class VehicleTypeDTO
    {
        public int Id { get; set; }
        public string NameType { get; set; }
        public string Note { get; set; }
        public int? NumOfSeat { get; set; }
        public bool? IsDeleted { get; set; }
    }
}
