using System;
using System.Collections.Generic;
using System.Text;
using Tim_Xe.Data.Repository.Entities;

namespace Tim_Xe.Data.Models
{
    public class GroupDTO
    {
        public GroupDTO() { }
        public GroupDTO(Group group)
        {
            this.Id = group.Id;
            this.Name = group.Name;
            this.Address = group.Address;
            this.CityName = group.IdCityNavigation.CityName;
            this.IdManager = group.IdManager;
            this.Status = group.Status;
            this.PriceCoefficient = group.PriceCoefficient;
            this.IsDeleted = group.IsDeleted;
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string CityName { get; set; }
        public int IdManager { get; set; }
        public string Status { get; set; }
        public double? PriceCoefficient { get; set; }
        public bool? IsDeleted { get; set; }
    }
}
