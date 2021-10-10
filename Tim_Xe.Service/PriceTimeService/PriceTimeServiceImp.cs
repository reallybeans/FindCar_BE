using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Tim_Xe.Data.Models;
using Tim_Xe.Data.Repository;
using Tim_Xe.Data.Repository.Entities;

namespace Tim_Xe.Service.PriceTimeService
{
    public class PriceTimeServiceImp
    {
        private readonly TimXeDBContext context;
        private readonly PriceTimeMapping priceTimeMapping;
        public PriceTimeServiceImp()
        {
            context = new TimXeDBContext();
            priceTimeMapping = new PriceTimeMapping();
        }
        public async Task<IEnumerable<PriceTimeDTO>> GetAllPriceTimesAsync()
        {
            return await context.PriceTimes.ProjectTo<PriceTimeDTO>(priceTimeMapping.configPriceTime).ToListAsync();
        }
        public async Task<PriceTimeDTO> GetPriceTimeByIdAsync(int id)
        {
            var result = await context.PriceTimes.ProjectTo<PriceTimeDTO>(priceTimeMapping.configPriceTime).FirstOrDefaultAsync(c => c.Id == id);
            return result;
        }
        public async Task<int> CreatePriceTime(PriceTimeCreateDTO priceTime)
        {
            try
            {
                var existingVehicleType = await context.VehicleTypes.FindAsync(priceTime.IdVehicleType);
                if (existingVehicleType == null)
                {
                    return 0;
                }
                else
                {
                    context.PriceTimes.Add(new PriceTime()
                    {
                        TimeWait = priceTime.TimeWait,
                        Price = priceTime.Price,
                        IdVehicleType = priceTime.IdVehicleType,
                        IsDeleted = false,
                    });
                }
            }
            catch(Exception e) {
                return 0;
            }
            return await context.SaveChangesAsync();
        }
        public async Task<int> UpdatePriceTime(PriceTimeUpdateDTO priceTime)
        {
            try
            {
                var existingPriceTime = await context.PriceTimes.FirstOrDefaultAsync(c => c.Id == priceTime.Id);
                var extstingVehicleType = await context.VehicleTypes.FindAsync(priceTime.IdVehicleType);
                if (extstingVehicleType == null)
                {
                    return 0;
                }
                if (existingPriceTime != null)
                {
                    existingPriceTime.TimeWait = priceTime.TimeWait;
                    existingPriceTime.Price = priceTime.Price;
                    existingPriceTime.IdVehicleType = priceTime.IdVehicleType;
                    existingPriceTime.IsDeleted = priceTime.IsDeleted;
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
        public async Task<bool> DeletePriceTimeAsync(int id)
        {
            var existingPriceTime = await context.PriceTimes.FirstOrDefaultAsync(c => c.Id == id);

            if (existingPriceTime != null)
            {
                existingPriceTime.IsDeleted = true;
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
