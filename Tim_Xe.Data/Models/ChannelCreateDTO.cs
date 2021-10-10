﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Tim_Xe.Data.Models
{
    public class ChannelCreateDTO
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter the name")]
        [StringLength(maximumLength: 50, MinimumLength = 3, ErrorMessage = "Length must be between 3 to 50")]
        public string Name { get; set; }
        public string Url { get; set; }
        public int? IdChannelType { get; set; }
        public int? IdGroup { get; set; }
        public string Status { get; set; }
    }
}
