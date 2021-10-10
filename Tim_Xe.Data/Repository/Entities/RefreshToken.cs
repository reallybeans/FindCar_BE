using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Tim_Xe.Data.Repository.Entities
{
    public partial class RefreshToken
    {
        public int TokenId { get; set; }
        public int? IdManager { get; set; }
        public string Token { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public int? IdDriver { get; set; }

        public virtual Driver IdDriverNavigation { get; set; }
        public virtual Manager IdManagerNavigation { get; set; }
    }
}
