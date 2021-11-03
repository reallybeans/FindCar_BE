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

namespace Tim_Xe.Service.PriceKmService
{
    public class PriceKmServiceImp : IPriceKmService
    {
        private readonly TimXeDBContext context;
        private readonly PriceKmMapping priceKmMapping;
        public PriceKmServiceImp()
        {
            context = new TimXeDBContext();
            priceKmMapping = new PriceKmMapping();
        }
        public async Task<PriceKmListDataDTO> GetAllPriceKmsAsync()
        {
            var result= await context.PriceKms.ProjectTo<PriceKmDTO>(priceKmMapping.configPriceKm).ToListAsync();
            if (result.Count() == 0)
            {
                return new PriceKmListDataDTO("list is empty", null, "empty");
            }
            else return new PriceKmListDataDTO("success", result, "success");
        }
        public async Task<PriceKmDataDTO> GetPriceKmByIdAsync(int id)
        {
            var result = await context.PriceKms.ProjectTo<PriceKmDTO>(priceKmMapping.configPriceKm).FirstOrDefaultAsync(c => c.Id == id);
            if (result == null)
            {
                return new PriceKmDataDTO("fail", null, "not Available");
            }
            else return new PriceKmDataDTO("success", result, "available");
        }
        public async Task<PriceKmCreateDataDTO> CreatePriceKm(PriceKmCreateDTO priceKm)
        {
            try
            {
                var existingVehicleType = await context.VehicleTypes.FindAsync(priceKm.IdVehicleType);
                if (existingVehicleType == null)
                {
                    return new PriceKmCreateDataDTO("fail", null, "create fail");
                }
                else
                {
                    context.PriceKms.Add(new PriceKm()
                    {
                        Km = (int)priceKm.Km,
                        Price = (double)priceKm.Price,
                        Description = priceKm.Description,
                        IdVehicleType = priceKm.IdVehicleType,
                        IsDeleted = false,
                    });
                    await context.SaveChangesAsync();
                    return new PriceKmCreateDataDTO("success", priceKm, "create success");
                }
            }
            catch(Exception e)
            {
                return new PriceKmCreateDataDTO("fail", null, "create fail");
            }
        }
        public async Task<PriceKmUpdateDataDTO> UpdatePriceKm(PriceKmUpdateDTO priceKm)
        {
            try
            {
                var existingPriceKm = await context.PriceKms.FirstOrDefaultAsync(c => c.Id == priceKm.Id);
                var extstingVehicleType = await context.VehicleTypes.FindAsync(priceKm.IdVehicleType);
                if (extstingVehicleType == null)
                {
                    return new PriceKmUpdateDataDTO("fail", null, "update fail");
                }
                if (existingPriceKm != null)
                {
                    existingPriceKm.Km = (int)priceKm.Km;
                    existingPriceKm.Price = (double)priceKm.Price;
                    existingPriceKm.Description = priceKm.Description;
                    existingPriceKm.IdVehicleType = priceKm.IdVehicleType;
                    existingPriceKm.IsDeleted = priceKm.IsDeleted;
                    context.PriceKms.Update(existingPriceKm);
                    await context.SaveChangesAsync();
                    return new PriceKmUpdateDataDTO("success", priceKm, "update success");
                }
                else
                {
                    return new PriceKmUpdateDataDTO("fail", null, "update fail");
                }
            }
            catch(Exception e)
            {
                return new PriceKmUpdateDataDTO("fail", null, "update fail");
            }
        }
        public async Task<bool> DeletePriceKmAsync(int id)
        {
            var existingPriceKm = await context.PriceKms.FirstOrDefaultAsync(c => c.Id == id);

            if (existingPriceKm != null)
            {
                context.PriceKms.Remove(existingPriceKm);
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
