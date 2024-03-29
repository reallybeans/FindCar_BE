﻿using System;
using Tim_Xe.Data.Repository.Entities;

namespace Tim_Xe.Data.Models
{
    public class DriverDTO : VehicleDTO
    {
        public DriverDTO(Driver driver, Vehicle vehicle)
        {
            Id = driver.Id;
            Name = driver.Name;
            Phone = driver.Phone;
            Email = driver.Email;
            CardId = driver.CardId;
            Img = driver.Img;
            IsDeleted = driver.IsDeleted;
            Status = driver.Status;
            CreateAt = driver.CreateAt;
            GroupId = driver.GroupId;
            Address = driver.Address;
            ReviewScore = driver.ReviewScore;
            //foreach (Vehicle x in vehicle)
            //{
            //    Vehicles.Add(new VehicleDTO(x.Id, x.Name, x.LicensePlate,x.IdVehicleType, x.Status));
            //}

            IdVehicle = vehicle.Id;
            NameVehicle = vehicle.Name;
            LicensePlate = vehicle.LicensePlate;
            IdVehicleType = vehicle.IdVehicleType;
            StatusVehicle = vehicle.Status;

        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string CardId { get; set; }
        public string Img { get; set; }
        public bool? IsDeleted { get; set; }
        public string Status { get; set; }
        public DateTime? CreateAt { get; set; }
        public int? GroupId { get; set; }
        public int? ReviewScore { get; set; }
        public string Address { get; set; }
    }
}
