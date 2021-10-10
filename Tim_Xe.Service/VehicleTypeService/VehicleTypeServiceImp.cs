using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Tim_Xe.Data.Models;
using Tim_Xe.Data.Repository;
using Tim_Xe.Data.Repository.Entities;

namespace Tim_Xe.Service.VehicleTypeService
{
    public class VehicleTypeServiceImp
    {
        private readonly TimXeDBContext context;
        private readonly VehicleTypeMapping vehicleTypeMapping;
        public VehicleTypeServiceImp()
        {
            context = new TimXeDBContext();
            vehicleTypeMapping = new VehicleTypeMapping();
        }
        public async Task<IEnumerable<VehicleTypeDTO>> GetAllVehicleTypesAsync()
        {
            return await context.VehicleTypes.ProjectTo<VehicleTypeDTO>(vehicleTypeMapping.configVehicleType).ToListAsync();
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
                existingVehicleType.IsDeleted = true;
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
