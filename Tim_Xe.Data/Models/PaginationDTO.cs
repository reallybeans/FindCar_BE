namespace Tim_Xe.Data.Models
{
    public class PaginationDTO
    {
        public int? Page { get; set; }
        public int? Size { get; set; }
        public string? SortOrder { get; set; }
        public string? SortField { get; set; }

    }
}
