namespace Tim_Xe.Data.Models
{
    public class GroupUpdateDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public int IdManager { get; set; }
        public string Status { get; set; }
        public double? PriceCoefficient { get; set; }
    }
}
