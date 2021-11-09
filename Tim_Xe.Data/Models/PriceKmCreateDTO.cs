namespace Tim_Xe.Data.Models
{
    public class PriceKmCreateDTO
    {
        public int? Km { get; set; }
        public double? Price { get; set; }
        public string Description { get; set; }
        public int? IdVehicleType { get; set; }
    }
}
