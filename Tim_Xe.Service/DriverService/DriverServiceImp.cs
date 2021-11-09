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
    public class DriverServiceImp : IDriverService
    {
        private readonly TimXeDBContext context;
        public DriverServiceImp()
        {
            context = new TimXeDBContext();
        }
        public async Task<DriverOnlySearchDataDTO> GetAllDriversAsync()
        {
            var driverExisted = await context.Drivers.Where(d => d.IsDeleted == false).ToListAsync();
            List<DriverOnlyDTO> driverDTO = new List<DriverOnlyDTO>();
            if (driverExisted.Count() != 0)
            {
                foreach (Driver x in driverExisted)
                {
                    driverDTO.Add(new DriverOnlyDTO(x));
                }
                return new DriverOnlySearchDataDTO("success", driverDTO, "success");
            }
            else return new DriverOnlySearchDataDTO("list is empty", null, "success");
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
        public async Task<DriverSearchDataDTO> SearchDriverAsync(DriverSearchDTO paging)
        {
            try
            {
                var driverExisted = new List<Driver>();
                List<DriverDTO> driverDTO = new List<DriverDTO>();
                Type t = typeof(Driver);
                PropertyInfo prop = t.GetProperty(paging.Pagination.SortField);
                if (paging.Pagination.SortOrder == "des")
                {
                    driverExisted = await context.Drivers.Where(m => m.Name.ToLower().Contains(paging.Name.ToLower()))
                    .OrderByDescending(m => m.Id)
                    .Skip((int)(paging.Pagination.Page * (paging.Pagination.Size)))
                    .Take((int)paging.Pagination.Size).ToListAsync();

                }
                else
                {
                    driverExisted = await context.Drivers.Where(m => m.Name.ToLower().Contains(paging.Name.ToLower()))
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
                return new DriverSearchDataDTO("success", driverDTO, "success");
            }
            catch (Exception e)
            {
                return new DriverSearchDataDTO("fail", null, "fail");
            }
        }
        public async Task<DriverDataDTO> GetDriverByIdAsync(int id)
        {
            var driverExisted = await context.Drivers.FirstOrDefaultAsync(m => m.Id == id);          
            if (driverExisted == null)
            {
                return new DriverDataDTO("fail", null, "not available");
            }
            else
            {
                var existingVehicle = await context.Vehicles.FirstOrDefaultAsync(g => g.Id == driverExisted.Id);
                DriverDTO driverDTO = new DriverDTO(driverExisted, existingVehicle);
                return new DriverDataDTO("success", driverDTO, driverExisted.Status);
            }
        }
        public async Task<DriverCreateDataDTO> CreateDriver(DriverCreateDTO driver)
        {
            try {
                var groupExisted = context.Groups.FirstOrDefault(g => g.IdManager == driver.CreateById);
                if (groupExisted == null) return new DriverCreateDataDTO("create fail", null, "fail");
                var validEmail = ValidateEmail.CheckEmail(driver.Email);
                var validPhone = ValiDatePhone.CheckPhone(driver.Phone);
                if (!validPhone) return new DriverCreateDataDTO("Phone number is exist", null, "fail");
                else if (!validEmail) return new DriverCreateDataDTO("email is exist", null, "fail");
                Driver drivers = new Driver();
                drivers.Name = driver.Name;
                drivers.Phone = driver.Phone;
                drivers.Email = driver.Email;
                drivers.CardId = driver.CardId;
                drivers.Img = driver.Img;
                drivers.Status = "off";
                drivers.Address = driver.Address;
                drivers.Latlng = driver.Latlng;
                drivers.IsDeleted = false;
                drivers.CreateAt = DateTime.Now;
                drivers.GroupId = groupExisted.Id;
                drivers.Revenue = 0;
                drivers.ReviewScore = 0;
                Vehicle vehicle = new Vehicle();
                vehicle.Name = driver.NameVehicle;
                vehicle.LicensePlate = driver.LicensePlate;
                vehicle.Status = "inuse";
                vehicle.IsDelete = false;
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
                    if (!driver.Phone.Contains(existingdrivers.Phone))
                    {
                        var validPhone = ValiDatePhone.CheckPhone(driver.Phone);
                        if (!validPhone) return new DriverUpdateDataDTO("Phone number is exist", null, "fail");
                    }
                    if (!driver.Email.Contains(existingdrivers.Email))
                    {
                        var validEmail = ValidateEmail.CheckEmail(driver.Email);
                        if (!validEmail) return new DriverUpdateDataDTO("email is exist", null, "fail");
                    }
                    existingdrivers.Name = driver.Name;
                    existingdrivers.Phone = driver.Phone;
                    existingdrivers.Email = driver.Email;
                    existingdrivers.CardId = driver.CardId;
                    existingdrivers.Img = driver.Img;
                    existingdrivers.IsDeleted = driver.IsDeleted;
                    existingdrivers.Address = driver.Address;
                    existingdrivers.Latlng = driver.Latlng;                   
                }
                context.Drivers.Update(existingdrivers);
                await context.SaveChangesAsync();
                return new DriverUpdateDataDTO("update succes", driver, "success");
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

        public async Task<DriverUpdateAddressDataDTO> UpdateAddress(DriverUpdateAddressDTO driverUpdateAddressDTO)
        {
            try
            {
                var existingdrivers = await context.Drivers.Include(d => d.Vehicles).FirstOrDefaultAsync(d => d.Id == driverUpdateAddressDTO.Id);
                if (existingdrivers != null)
                {
                    existingdrivers.Address = driverUpdateAddressDTO.Address;
                    existingdrivers.Latlng = driverUpdateAddressDTO.Latlng;
                    context.Drivers.Update(existingdrivers);
                    await context.SaveChangesAsync();
                }
                context.Drivers.Update(existingdrivers);
                await context.SaveChangesAsync();
                return new DriverUpdateAddressDataDTO("update succes", driverUpdateAddressDTO, "success");
            }
            catch (Exception e)
            {
                return new DriverUpdateAddressDataDTO("update fail", null, "fail");
            }
        }
        public async Task<DriverUpdateStatusDataDTO> UpdateStatus(DriverUpdateStatusDTO driverUpdate)
        {
            try
            {
                var existingdrivers = await context.Drivers.Include(d => d.Vehicles).FirstOrDefaultAsync(d => d.Id == driverUpdate.Id);
                if (existingdrivers != null)
                {
                    existingdrivers.Status = driverUpdate.Status;
                    context.Drivers.Update(existingdrivers);
                    await context.SaveChangesAsync();
                    return new DriverUpdateStatusDataDTO("update success", driverUpdate, "success");
                }
                else return new DriverUpdateStatusDataDTO("update fail", null, "fail");
            }
            catch (Exception e)
            {
                return new DriverUpdateStatusDataDTO("update fail", null, "fail");
            }
        }
        public async Task<DriverOnlySearchDataDTO> SearchByName(string name)
        {
            try
            {
                List<DriverOnlyDTO> driverDTO = new List<DriverOnlyDTO>();
                var driverExisted = new List<Driver>();
                if (name == null)
                {
                    driverExisted = await context.Drivers.ToListAsync();
                }
                else
                {
                    driverExisted = await context.Drivers.Where(d => d.Name.ToLower().Contains(name)).ToListAsync();
                }
                if (driverExisted.Count() != 0)
                {
                    foreach(Driver x in driverExisted)
                    {
                        driverDTO.Add(new DriverOnlyDTO(x));
                    }
                    return new DriverOnlySearchDataDTO("success", driverDTO, "success");
                }
                else return new DriverOnlySearchDataDTO("list is empty", null, "success");
            }
            catch (Exception e)
            {
                return new DriverOnlySearchDataDTO("fail", null, "fail");
            }
        }
        public async Task<DriverOnlySearchDataDTO> SearchByPhone(string phone)
        {
            try
            {
                List<DriverOnlyDTO> driverDTO = new List<DriverOnlyDTO>();
                var driverExisted = new List<Driver>();
                if (phone != null) {
                   driverExisted = await context.Drivers.Where(d => d.Phone.Contains(phone)).ToListAsync();
                }
                else
                {
                    driverExisted = await context.Drivers.ToListAsync();
                }
                if (driverExisted.Count() != 0)
                {
                    foreach (Driver x in driverExisted)
                    {
                        driverDTO.Add(new DriverOnlyDTO(x));
                    }
                    return new DriverOnlySearchDataDTO("success", driverDTO, "success");
                }
                else return new DriverOnlySearchDataDTO("list is empty", null, "success");
            }
            catch (Exception e)
            {
                return new DriverOnlySearchDataDTO("fail", null, "fail");
            }
        }

        public async Task<IEnumerable<DriverOnlyDTO>> SearchDrivers(string search)
        {
            try
            {
                List<DriverOnlyDTO> driverDTO = new List<DriverOnlyDTO>();
                var driverExisted = new List<Driver>();
                if (search == null)  driverExisted = await context.Drivers.Where(d => d.IsDeleted == false).ToListAsync();
                else
                {
                    driverExisted = await context.Drivers.Where(d => (d.IsDeleted == false) && ((d.Name.Contains(search.ToLower())) || (d.Phone.Contains(search)))).ToListAsync();
                }
                if (driverExisted.Count() != 0)
                {
                    foreach (Driver x in driverExisted)
                    {
                        driverDTO.Add(new DriverOnlyDTO(x));
                    }                 
                }
                return driverDTO;
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}
