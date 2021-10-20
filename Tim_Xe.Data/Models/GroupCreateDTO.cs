using System;
using System.Collections.Generic;
using System.Text;

namespace Tim_Xe.Data.Models
{
    public class GroupCreateDTO
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string District { get; set; }
        public int IdManager { get; set; }
        public string? City { get; set; }
        public string Status { get; set; }
        public double? PriceCoefficient { get; set; }
    }
}
