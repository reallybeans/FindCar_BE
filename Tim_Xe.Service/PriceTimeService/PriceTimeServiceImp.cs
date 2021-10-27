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
        public async Task<PriceTimeDataDTO> GetPriceTimeByIdAsync(int id)
        {
            var result = await context.PriceTimes.ProjectTo<PriceTimeDTO>(priceTimeMapping.configPriceTime).FirstOrDefaultAsync(c => c.Id == id);
            if (result == null)
            {
                return new PriceTimeDataDTO("fail", result, "not available");
            }
            else
            {
                return new PriceTimeDataDTO("success", result, "available");
            }
        }
        public async Task<PriceTimeCreateDataDTO> CreatePriceTime(PriceTimeCreateDTO priceTime)
        {
            try
            {
                var existingVehicleType = await context.VehicleTypes.FindAsync(priceTime.IdVehicleType);
                if (existingVehicleType == null)
                {
                    return new PriceTimeCreateDataDTO("fail", null,"create fail");
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
                    await context.SaveChangesAsync();
                    return new PriceTimeCreateDataDTO("success", priceTime, "create success");
                }
            }
            catch(Exception e) {
                return new PriceTimeCreateDataDTO("fail", null, "create fail");
            }
        }
        public async Task<PriceTimeUpdateDataDTO> UpdatePriceTime(PriceTimeUpdateDTO priceTime)
        {
            try
            {
                var existingPriceTime = await context.PriceTimes.FirstOrDefaultAsync(c => c.Id == priceTime.Id);
                var extstingVehicleType = await context.VehicleTypes.FindAsync(priceTime.IdVehicleType);
                if (extstingVehicleType == null)
                {
                    return new PriceTimeUpdateDataDTO("fail", null, "update fail");
                }
                if (existingPriceTime != null)
                {
                    existingPriceTime.TimeWait = priceTime.TimeWait;
                    existingPriceTime.Price = priceTime.Price;
                    existingPriceTime.IdVehicleType = priceTime.IdVehicleType;
                    existingPriceTime.IsDeleted = priceTime.IsDeleted;
                    context.PriceTimes.Update(existingPriceTime);
                    await context.SaveChangesAsync();
                    return new PriceTimeUpdateDataDTO("success", priceTime, "update success");                  
                }
                else
                {
                    return new PriceTimeUpdateDataDTO("fail", null, "update fail");
                }
            }
            catch(Exception e)
            {
                return new PriceTimeUpdateDataDTO("fail", null, "update fail");
            }

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
