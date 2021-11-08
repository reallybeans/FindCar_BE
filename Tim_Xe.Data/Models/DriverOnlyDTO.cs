using System;
using System.Collections.Generic;
using System.Text;
using Tim_Xe.Data.Repository.Entities;

namespace Tim_Xe.Data.Models
{
    public class DriverOnlyDTO
    {
        public DriverOnlyDTO(Driver driver)
        {
            Id = driver.Id;
            Name = driver.Name;
            Phone = driver.Phone;
            Email = driver.Email;
            Address = driver.Address;
            Latlng = driver.Latlng;
            CardId = driver.CardId;
            Img = driver.Img;
            IsDeleted = driver.IsDeleted;
            Status = driver.Status;
            CreateAt = driver.CreateAt;
            GroupId = driver.GroupId;
            Revenue = driver.Revenue;
            ReviewScore = driver.ReviewScore;
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Latlng { get; set; }
        public string CardId { get; set; }
        public string Img { get; set; }
        public bool? IsDeleted { get; set; }
        public string Status { get; set; }
        public DateTime? CreateAt { get; set; }
        public int? GroupId { get; set; }
        public double? Revenue { get; set; }
        public int? ReviewScore { get; set; }
    }
}
