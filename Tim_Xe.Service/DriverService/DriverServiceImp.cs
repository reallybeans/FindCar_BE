using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Tim_Xe.Data.Models;
using Tim_Xe.Data.Repository;
using Tim_Xe.Data.Repository.Entities;
using Tim_Xe.Service.Shared;

namespace Tim_Xe.Service.DriverService
{
    public class DriverServiceImp
    {
        private readonly TimXeDBContext context;
        public DriverServiceImp()
        {
            context = new TimXeDBContext();
        }
        public async Task<DriverListDataDTO> GetAllDriversAsync()
        {
            var driverExisted = await context.Drivers.ToListAsync();
            List<DriverDTO> driverDTO = new List<DriverDTO>();
            foreach (Driver x in driverExisted)
            {
                var existingVehicle = await context.Vehicles.FirstOrDefaultAsync(g => g.Id == x.Id);
                if(existingVehicle != null)
                driverDTO.Add(new DriverDTO(x , existingVehicle));
            }
            if (driverDTO.Count() == 0)
            {
                return new DriverListDataDTO("list is empty", null, "empty");
            }
            else return new DriverListDataDTO("success", driverDTO, "success");
        }
        public async Task<DriverListDataDTO> GetAllDriversByIdManagerAsync(int id)
        {
            var groupExisted = context.Groups.FirstOrDefault(g => g.IdManager == id);
            if (groupExisted == null) return new DriverListDataDTO("list is empty", null, "empty");
            var driverExisted = await context.Drivers.Where(d => d.GroupId == groupExisted.Id).ToListAsync();
            List<DriverDTO> driverDTO = new List<DriverDTO>();
            foreach (Driver x in driverExisted)
            {
                var existingVehicle = await context.Vehicles.FirstOrDefaultAsync(g => g.Id == x.Id);
                if (existingVehicle != null)
                    driverDTO.Add(new DriverDTO(x, existingVehicle));
            }
            if (driverDTO.Count() == 0)
            {
                return new DriverListDataDTO("list is empty", null, "empty");
            }
            else return new DriverListDataDTO("success", driverDTO, "success");
        }
        public async Task<IEnumerable<DriverDTO>> SearchDriverAsync(DriverSearchDTO paging)
        {
            var driverExisted = new List<Driver>();
            List<DriverDTO> driverDTO = new List<DriverDTO>();

            Type t = typeof(Driver);
            PropertyInfo prop = t.GetProperty(paging.Pagination.SortField);
            if (paging.Pagination.SortOrder == "des")
            {
              driverExisted = await context.Drivers.Where(m => m.Name.Contains(paging.Name))
              .OrderByDescending(m => m.Id)
              .Skip((int)(paging.Pagination.Page * (paging.Pagination.Size)))
              .Take((int)paging.Pagination.Size).ToListAsync();

            }
            else
            {
               driverExisted = await context.Drivers.Where(m => m.Name.Contains(paging.Name))
              .OrderBy(m => m.Id)
              .Skip((int)(paging.Pagination.Page * (paging.Pagination.Size)))
              .Take((int)paging.Pagination.Size).ToListAsync();
            }
            foreach (Driver x in driverExisted)
            {
                var existingVehicle = await context.Vehicles.FirstOrDefaultAsync(g => g.Id == x.Id);
                if (existingVehicle != null)
                    driverDTO.Add(new DriverDTO(x, existingVehicle));
            }
            return driverDTO;
        }
        public async Task<DriverDataDTO> GetDriverByIdAsync(int id)
        {
            var driverExisted = await context.Drivers.FirstOrDefaultAsync(m => m.Id == id);
            var existingVehicle = await context.Vehicles.FirstOrDefaultAsync(g => g.Id == driverExisted.Id);
            DriverDTO driverDTO = new DriverDTO(driverExisted, existingVehicle);
            if (driverExisted == null)
            {
                return new DriverDataDTO("fail", null, "not available");
            }
            else
            {
                return new DriverDataDTO("success", driverDTO, driverExisted.Status);
            }
        }
        public async Task<DriverCreateDataDTO> CreateDriver(DriverCreateDTO driver)
        {
            try {
                var groupExisted = context.Groups.FirstOrDefault(g => g.IdManager == driver.CreateById);
                if (groupExisted == null) return new DriverCreateDataDTO("create fail", null, "fail");
                Driver drivers = new Driver();
                drivers.Name = driver.Name;
                drivers.Phone = driver.Phone;
                drivers.Email = driver.Email;
                drivers.CardId = driver.CardId;
                drivers.Img = driver.Img;
                drivers.IsDeleted = true;
                drivers.Status = driver.Status;
                drivers.Address = driver.Address;
                drivers.Latlng = driver.Latlng;
                drivers.IsDeleted = true;
                drivers.CreateAt = DateTime.Now;
                drivers.GroupId = groupExisted.Id;
                Vehicle vehicle = new Vehicle();
                vehicle.Name = driver.NameVehicle;
                vehicle.LicensePlate = driver.LicensePlate;
                vehicle.Status = driver.StatusVehicle;
                var VehicleType = await context.VehicleTypes.FirstOrDefaultAsync(d => d.NameType == driver.VehicleType);
                vehicle.IdVehicleType = VehicleType.Id;

                if (VehicleType == null)
                {
                    return new DriverCreateDataDTO("create fail", null, "fail");
                }
                else
                {
                    drivers.Vehicles.Add(vehicle);
                    context.Drivers.Add(drivers);
                    await context.SaveChangesAsync();
                    return new DriverCreateDataDTO("create success", driver, "success");
                }                                
            } catch (Exception e) {
                return new DriverCreateDataDTO("create fail", null, "fail");
            }
        }
        public async Task<DriverUpdateDataDTO> UpdateDriver(DriverUpdateDTO driver)
        {
            try
            {
                var existingdrivers = await context.Drivers.Include(d => d.Vehicles).FirstOrDefaultAsync(d => d.Id == driver.Id);
                if (existingdrivers != null)
                {
                    existingdrivers.Name = driver.Name;
                    existingdrivers.Phone = driver.Phone;
                    existingdrivers.Email = driver.Email;
                    existingdrivers.CardId = driver.CardId;
                    existingdrivers.Img = driver.Img;
                    existingdrivers.Status = driver.Status;
                    existingdrivers.Address = driver.Address;
                    existingdrivers.Latlng = driver.Latlng;
                    foreach (Vehicle vehicle in existingdrivers.Vehicles)
                    {
                        vehicle.Name = driver.NameVehicle;
                        vehicle.LicensePlate = driver.LicensePlate;
                        vehicle.Status = driver.StatusVehicle;
                        var VehicleType = await context.VehicleTypes.FirstOrDefaultAsync(d => d.NameType == driver.VehicleType);
                        if(VehicleType == null)
                        {
                            return new DriverUpdateDataDTO("update fail", null, "fail");
                        }
                        else
                        {
                            vehicle.IdVehicleType = VehicleType.Id;
                            context.Vehicles.Update(vehicle);
                        }
                    }
                }
                context.Drivers.Update(existingdrivers);
                await context.SaveChangesAsync();
                return new DriverUpdateDataDTO("update succes",driver,"success");
            }
            catch (Exception e) {
                return new DriverUpdateDataDTO("update fail", null, "fail");
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
