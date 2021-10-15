using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tim_Xe.Data.Models;
using Tim_Xe.Data.Repository;
using Tim_Xe.Data.Repository.Entities;

namespace Tim_Xe.Service.DriverService
{
    public class DriverServiceImp
    {
        private readonly TimXeDBContext context;
        private readonly DriverMapping driverMapping;
        public DriverServiceImp()
        {
            context = new TimXeDBContext();
        }
        public async Task<IEnumerable<DriverDTO>> GetAllDriversAsync()
        {
            var driverExisted = await context.Drivers.ToListAsync();
            List<DriverDTO> driverDTO = new List<DriverDTO>();
            foreach (Driver x in driverExisted)
            {
                var existingVehicle = await context.Vehicles.FirstOrDefaultAsync(g => g.Id == x.Id);
                driverDTO.Add(new DriverDTO(x , existingVehicle));
            }
            return driverDTO;
        }
        public async Task<IEnumerable<DriverDTO>> SearchDriverAsync(DriverSearchDTO paging)
        {
            if (paging.Pagination.SortOrder == "des")
            {
                return await context.Drivers
               .Where(m => m.Name.Contains(paging.Name))
               .OrderByDescending(m => m.Id)
               .Skip((int)(paging.Pagination.Page * (paging.Pagination.Size)))
               .Take((int)paging.Pagination.Size)
               .ProjectTo<DriverDTO>(driverMapping.configDriver)
               .ToListAsync();
            }
            else
            {
                return await context.Drivers
                               .Where(m => m.Name.Contains(paging.Name))
                               .OrderBy(m => m.Id)
                               .Skip((int)(paging.Pagination.Page * (paging.Pagination.Size)))
                               .Take((int)paging.Pagination.Size)
                               .ProjectTo<DriverDTO>(driverMapping.configDriver)
                               .ToListAsync();
            }

        }
        public async Task<DriverDTO> GettDriverByIdAsync(int id)
        {
            var driverExisted = await context.Drivers.FirstOrDefaultAsync(m => m.Id == id);
                var existingVehicle = await context.Vehicles.FirstOrDefaultAsync(g => g.Id == driverExisted.Id);
            DriverDTO driverDTO = new DriverDTO(driverExisted, existingVehicle);
            return driverDTO;
        }
        public async Task<bool> CreateDriver(DriverCreateDTO driver)
        {
            try {
                Driver drivers = new Driver();
                drivers.Name = driver.Name;
                drivers.Phone = driver.Phone;
                drivers.Email = driver.Email;
                drivers.CardId = driver.CardId;
                drivers.Img = driver.Img;
                drivers.IsDeleted = true;
                drivers.Status = driver.Status;
                drivers.IsDeleted = true;
                drivers.CreateAt = DateTime.Now;
                drivers.CreateById = driver.CreateById;
                Vehicle vehicle = new Vehicle();
                vehicle.Name = driver.NameVehicle;
                vehicle.LicensePlate = driver.LicensePlate;
                vehicle.Status = driver.StatusVehicle;
                var VehicleType = await context.VehicleTypes.FirstOrDefaultAsync(d => d.NameType == driver.VehicleType);
                vehicle.IdVehicleType = VehicleType.Id;
                
                drivers.Vehicles.Add(vehicle);
                context.Drivers.Add(drivers);
                await context.SaveChangesAsync();
                
            } catch (Exception e) {
                return false;
            }
            
            return true;
        }
        public async Task<bool> UpdateDriver(DriverUpdateDTO driver)
        {
            try
            {
                var existingdrivers = await context.Drivers.FirstOrDefaultAsync(d => d.Id == driver.Id);
                if (existingdrivers != null)
                {
                    existingdrivers.Name = driver.Name;
                    existingdrivers.Phone = driver.Phone;
                    existingdrivers.Email = driver.Email;
                    existingdrivers.CardId = driver.CardId;
                    existingdrivers.Img = driver.Img;
                    existingdrivers.Status = driver.Status;
                    foreach (Vehicle vehicle in existingdrivers.Vehicles)
                    {
                        vehicle.Name = driver.NameVehicle;
                        vehicle.LicensePlate = driver.LicensePlate;
                        vehicle.Status = driver.StatusVehicle;
                        var VehicleType = await context.VehicleTypes.FirstOrDefaultAsync(d => d.NameType == driver.VehicleType);
                        vehicle.IdVehicleType = VehicleType.Id;
                    }
                }
                await context.SaveChangesAsync();
                return true;
            }
            catch (Exception e) {
                return false;
            }
        }
        public async Task<bool> DeleteDriverAsync(int id)
        {
            var existingdriver = await context.Drivers.FirstOrDefaultAsync(m => m.Id == id);

            if (existingdriver != null)
            {
                existingdriver.IsDeleted = true;
                await context.SaveChangesAsync();

                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
