using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Tim_Xe.Data.Repository.Entities
{
    public partial class Channel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public int? IdChannelType { get; set; }
        public int? IdGroup { get; set; }
        public bool? IsDeleted { get; set; }
        public string Status { get; set; }

        public virtual ChannelType IdChannelTypeNavigation { get; set; }
        public virtual Group IdGroupNavigation { get; set; }
    }
}
