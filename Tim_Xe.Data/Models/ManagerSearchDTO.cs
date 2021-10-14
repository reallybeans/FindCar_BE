using System;
using System.Collections.Generic;
using System.Text;

namespace Tim_Xe.Data.Models
{
    public class ManagerSearchDTO
    {
        public string? Email { get; set; }
        public PaginationDTO Pagination { get; set; }
    }
}
