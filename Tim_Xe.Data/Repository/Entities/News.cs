using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Tim_Xe.Data.Repository.Entities
{
    public partial class News
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
        public int? IdGroup { get; set; }
        public DateTime? CreateAt { get; set; }

        public virtual Group IdGroupNavigation { get; set; }
    }
}
