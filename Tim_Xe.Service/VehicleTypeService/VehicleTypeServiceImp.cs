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

namespace Tim_Xe.Service.VehicleTypeService
{
    public class VehicleTypeServiceImp : IVehicleTypeService
    {
        private readonly TimXeDBContext context;
        private readonly VehicleTypeMapping vehicleTypeMapping;
        public VehicleTypeServiceImp()
        {
            context = new TimXeDBContext();
            vehicleTypeMapping = new VehicleTypeMapping();
        }
        public async Task<VehicleTypeListDataDTO> GetAllVehicleTypesAsync()
        {
            var result= await context.VehicleTypes.ProjectTo<VehicleTypeDTO>(vehicleTypeMapping.configVehicleType).ToListAsync();
            if (result.Count() == 0)
            {
                return new VehicleTypeListDataDTO("list is empty", null, "empty");
            }
            else return new VehicleTypeListDataDTO("success", result, "success");
        }
        public async Task<VehicleTypeDTO> GetVehicleTypeByIdAsync(int id)
        {
            var result = await context.VehicleTypes.ProjectTo<VehicleTypeDTO>(vehicleTypeMapping.configVehicleType).FirstOrDefaultAsync(c => c.Id == id);
            return result;
        }
        public async Task<int> CreateVehicleType(VehicleTypeCreateDTO vehicleType)
        {
            try
            {
                context.VehicleTypes.Add(new VehicleType()
                {
                    NameType = vehicleType.NameType,
                    Note = vehicleType.Note,
                    NumOfSeat = vehicleType.NumOfSeat,
                    IsDeleted = false
                }); ;
            }
            catch(Exception e)
            {
                return 0;
            }
            return await context.SaveChangesAsync();
        }
        public async Task<int> UpdateVehicleType(VehicleTypeUpdateDTO vehicleType)
        {
            try
            {
                var existingVehicleType = await context.VehicleTypes.FirstOrDefaultAsync(c => c.Id == vehicleType.Id);
                if (existingVehicleType != null)
                {
                    existingVehicleType.NameType = vehicleType.NameType;
                    existingVehicleType.Note = vehicleType.Note;
                    existingVehicleType.NumOfSeat = vehicleType.NumOfSeat;
                    existingVehicleType.IsDeleted = vehicleType.IsDeleted;
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception e)
            {
                return 0;
            }
            return await context.SaveChangesAsync();
        }
        public async Task<bool> DeleteVehicleTypeAsync(int id)
        {
            var existingVehicleType = await context.VehicleTypes.FirstOrDefaultAsync(c => c.Id == id);

            if (existingVehicleType != null)
            {
                context.VehicleTypes.Remove(existingVehicleType);
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
