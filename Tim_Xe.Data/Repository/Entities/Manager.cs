using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Tim_Xe.Data.Repository.Entities
{
    public partial class Manager
    {
        public Manager()
        {
            Drivers = new HashSet<Driver>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public int RoleId { get; set; }
        public string Status { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime? CreateAt { get; set; }
        public string CardId { get; set; }
        public string Img { get; set; }

        public virtual Role Role { get; set; }
        public virtual ICollection<Driver> Drivers { get; set; }
    }
}
