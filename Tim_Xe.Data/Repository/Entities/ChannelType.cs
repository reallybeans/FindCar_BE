using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Tim_Xe.Data.Repository.Entities
{
    public partial class ChannelType
    {
        public ChannelType()
        {
            Channels = new HashSet<Channel>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Channel> Channels { get; set; }
    }
}
