using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Tim_Xe.Data.Repository.Entities
{
    public partial class Role
    {
        public Role()
        {
            Managers = new HashSet<Manager>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Manager> Managers { get; set; }
    }
}
