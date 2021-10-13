using System;
using System.Collections.Generic;
using System.Text;

namespace Tim_Xe.Data.Models
{
    public class ScheduleDTO
    {
        public int Total { get; set; }
        public List<AddressDTO> Address { get; set; }
        public List<LatlngDTO> Latlng { get; set; }
    }
}
