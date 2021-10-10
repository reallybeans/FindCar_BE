using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Tim_Xe.Data.Models;
using Tim_Xe.Data.Repository;
using Tim_Xe.Data.Repository.Entities;

namespace Tim_Xe.Service.PriceKmService
{
    public class PriceKmServiceImp
    {
        private readonly TimXeDBContext context;
        private readonly PriceKmMapping priceKmMapping;
        public PriceKmServiceImp()
        {
            context = new TimXeDBContext();
            priceKmMapping = new PriceKmMapping();
        }
        public async Task<IEnumerable<PriceKmDTO>> GetAllPriceKmsAsync()
        {
            return await context.PriceKms.ProjectTo<PriceKmDTO>(priceKmMapping.configPriceKm).ToListAsync();
        }
        public async Task<PriceKmDTO> GetPriceKmByIdAsync(int id)
        {
            var result = await context.PriceKms.ProjectTo<PriceKmDTO>(priceKmMapping.configPriceKm).FirstOrDefaultAsync(c => c.Id == id);
            return result;
        }
        public async Task<int> CreatePriceKm(PriceKmCreateDTO priceKm)
        {
            try
            {
                var existingVehicleType = await context.VehicleTypes.FindAsync(priceKm.IdVehicleType);
                if (existingVehicleType == null)
                {
                    return 0;
                }
                else
                {
                    context.PriceKms.Add(new PriceKm()
                    {
                        Km = priceKm.Km,
                        Price = priceKm.Price,
                        Description = priceKm.Description,
                        IdVehicleType = priceKm.IdVehicleType,
                        IsDeleted = false,
                    });
                }
            }
            catch(Exception e)
            {
                return 0;
            }
            return await context.SaveChangesAsync();
        }
        public async Task<int> UpdatePriceKm(PriceKmUpdateDTO priceKm)
        {
            try
            {
                var existingPriceKm = await context.PriceKms.FirstOrDefaultAsync(c => c.Id == priceKm.Id);
                var extstingVehicleType = await context.VehicleTypes.FindAsync(priceKm.IdVehicleType);
                if (extstingVehicleType == null)
                {
                    return 0;
                }
                if (existingPriceKm != null)
                {
                    existingPriceKm.Km = priceKm.Km;
                    existingPriceKm.Price = priceKm.Price;
                    existingPriceKm.Description = priceKm.Description;
                    existingPriceKm.IdVehicleType = priceKm.IdVehicleType;
                    existingPriceKm.IsDeleted = priceKm.IsDeleted;
                }
                else
                {
                    return 0;
                }
            }
            catch(Exception e)
            {
                return 0;
            }

            return await context.SaveChangesAsync();
        }
        public async Task<bool> DeletePriceKmAsync(int id)
        {
            var existingPriceKm = await context.PriceKms.FirstOrDefaultAsync(c => c.Id == id);

            if (existingPriceKm != null)
            {
                existingPriceKm.IsDeleted = true;
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
