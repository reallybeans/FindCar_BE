using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tim_Xe.Data.Models;
using Tim_Xe.Data.Repository;
using Tim_Xe.Data.Repository.Entities;
using Tim_Xe.Service.Shared;

namespace Tim_Xe.Service.VehiclesService
{
    public class VehiclesServiceImp : IVehiclesService
    {
        private readonly TimXeDBContext context;
        private readonly RemoveUnicode removeUnicode;
        public VehiclesServiceImp()
        {
            context = new TimXeDBContext();
            removeUnicode = new RemoveUnicode();
        }

        public async Task<VehiclesCreateDataDTO> AddVehicle(VehicleCreateDTO vehicleCreateDTO)
        {
            try
            {
                vehicleCreateDTO.VehicleType = removeUnicode.RemoveSign4VietnameseString(vehicleCreateDTO.VehicleType);
                var driverExisted = context.Drivers.Include(d => d.Vehicles).Where(d => d.Id == vehicleCreateDTO.DriverId).FirstOrDefault();
                if (driverExisted == null) return new VehiclesCreateDataDTO("driver not existed", null, "fail");
                driverExisted.Vehicles.Add(new Vehicle()
                {
                    Name = vehicleCreateDTO.VehicleName,
                    LicensePlate = vehicleCreateDTO.LicensePlate,
                    IdVehicleType = context.VehicleTypes.Where(v => v.NameType == vehicleCreateDTO.VehicleType).Select(v => v.Id).FirstOrDefault(),
                    Status = "unuse",
                    IsDelete = false
                });
                await context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return new VehiclesCreateDataDTO("create fail", null, "fail");
            }
            return new VehiclesCreateDataDTO("create success", vehicleCreateDTO, "success");
        }

        public async Task<VehiclesUpdateDataDTO> EditVehicle(VehiclesUpdateDTO vehiclesUpdateDTO)
        {
            try
            {
                vehiclesUpdateDTO.VehicleType = removeUnicode.RemoveSign4VietnameseString(vehiclesUpdateDTO.VehicleType);
                var vehicleExisted = context.Vehicles.Where(d => d.Id == vehiclesUpdateDTO.Id).FirstOrDefault();
                if (vehicleExisted == null) return new VehiclesUpdateDataDTO("Failure to get vehicle", null, "false");
                vehicleExisted.Name = vehiclesUpdateDTO.VehicleName;
                vehicleExisted.LicensePlate = vehiclesUpdateDTO.LicensePlate;
                vehicleExisted.IdVehicleType = context.VehicleTypes.Where(v => v.NameType == vehiclesUpdateDTO.VehicleType).Select(v => v.Id).FirstOrDefault();
                vehicleExisted.IsDelete = vehiclesUpdateDTO.IsDelete;
                context.Update(vehicleExisted);
                context.SaveChanges();
            }
            catch (Exception e) {
                return new VehiclesUpdateDataDTO("Failure to update vehicle", null, "false");
            }
           

            return new VehiclesUpdateDataDTO("update success", vehiclesUpdateDTO, "success");
        }

        public async Task<VehiclesDataDTO> GetAllVehicle()
        {
            var list = await getListVehiclesAsync(0);
            if (list == null)
                return new VehiclesDataDTO("Failure to get vehicles", null, "false");
            return new VehiclesDataDTO("success", list, "success"); ;
        }

        public async Task<VehiclesDataDTO> GetAllVehicleByIdManager(int idManager)
        {
            var list = await getListVehiclesAsync(idManager);
            if (list == null)
                return new VehiclesDataDTO("Failure to get vehicles", null, "false");
            return new VehiclesDataDTO("success", list, "success"); ;
        }
        public async Task<List<VehiclesDTO>> getListVehiclesAsync(int id)
        {
            var VehiclesExisted = new List<Vehicle>();
            if (id != 0)
            {
                var ManagerExisted = await context.Managers.Where(m => m.Id == id && m.IsDeleted == false)
                    .Include(m => m.Groups)
                    .ThenInclude(g => g.Drivers)
                    .ThenInclude(d => d.Vehicles)
                    .FirstOrDefaultAsync();
                foreach (Driver x in ManagerExisted.Groups.ElementAt(0).Drivers)
                    foreach (Vehicle y in x.Vehicles)
                        if ((bool)!y.IsDelete)
                            VehiclesExisted.Add(y);
            }
            else
                VehiclesExisted = await context.Vehicles.Where(v => v.IsDelete == false).ToListAsync();
            if (VehiclesExisted == null)
                return null;
            List<VehiclesDTO> list = new List<VehiclesDTO>();

            foreach (Vehicle x in VehiclesExisted)
            {
                VehiclesDTO vehicles = new VehiclesDTO();
                vehicles.Id = x.Id;
                vehicles.DriverName = await context.Drivers.Where(d => d.Id == x.IdDriver).Select(d => d.Name)
                    .FirstOrDefaultAsync();
                vehicles.VehicleName = x.Name;
                vehicles.LicensePlate = x.LicensePlate;
                vehicles.VehicleType = await context.VehicleTypes.Where(v => v.Id == x.IdVehicleType).Select(v => v.NameType)
                    .FirstOrDefaultAsync();
                vehicles.Status = x.Status;
                list.Add(vehicles);
            }
            return list;
        }
    }
}
