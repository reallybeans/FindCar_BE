﻿using System.Collections.Generic;

namespace Tim_Xe.Data.Models
{
    public class LatlngDTO
    {
        public string? Origin { get; set; }
        public string? Destination { get; set; }
        public string? Waypoint { get; set; }
        public List<string>? ListWaypoint { get; set; }
    }
}

