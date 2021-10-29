using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Tim_Xe.Data.Repository.Entities
{
    public partial class City
    {
        public City()
        {
            Groups = new HashSet<Group>();
        }

        public int Id { get; set; }
        public string CityName { get; set; }
        public bool? IsDeleted { get; set; }

        public virtual ICollection<Group> Groups { get; set; }
    }
}
