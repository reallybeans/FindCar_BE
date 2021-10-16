using System;
using System.Collections.Generic;
using System.Text;

namespace Tim_Xe.Data.Models
{
    public class CustomerSearchDTO
    {
        public string Name { get; set; }
        public PaginationDTO Pagination { get; set; }
    }
}
