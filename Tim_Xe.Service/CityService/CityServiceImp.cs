using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Tim_Xe.Data.Models;
using Tim_Xe.Data.Repository;
using Tim_Xe.Data.Repository.Entities;

namespace Tim_Xe.Service.CityService
{
    public class CityServiceImp : ICityService
    {
        private readonly TimXeDBContext context;
        private readonly CityMapping cityMapping;
        public CityServiceImp()
        {
            context = new TimXeDBContext();
            cityMapping = new CityMapping();
        }
        public async Task<CityCreateDataDTO> CreateCity(CityCreateDTO cityCreateDTO)
        {
            try
            {
                context.Cities.Add(new City()
                {
                    CityName = cityCreateDTO.Name,
                    IsDeleted = false,
                });
                await context.SaveChangesAsync();
                return new CityCreateDataDTO("create success", cityCreateDTO, "success");
            }
            catch (Exception e)
            {
                return new CityCreateDataDTO("create fail", null, "fail");
            }
        }

        public async Task<bool> DeleteCityAsync(int id)
        {
            var existingCity = await context.Cities.ProjectTo<CityDTO>(cityMapping.configCity).FirstOrDefaultAsync(c => c.Id == id);
            if (existingCity == null)
            {
                existingCity.IsDeleted = false;
                await context.SaveChangesAsync();
                return true;
            }
            else return false;
        }

        public async Task<CityListDataDTO> GetAllCitiesAsync()
        {
            var result = await context.Cities.ProjectTo<CityDTO>(cityMapping.configCity).ToListAsync();
            if (result.Count() == 0)
            {
                return new CityListDataDTO("list is empty", null, "empty");
            }
            else return new CityListDataDTO("success", result, "success");
        }

        public async Task<CityDataDTO> GetCityByIdAsync(int id)
        {
            var result = await context.Cities.ProjectTo<CityDTO>(cityMapping.configCity).FirstOrDefaultAsync(c => c.Id == id);
            if (result == null)
            {
                return new CityDataDTO("city is not available", null, "not available");
            }
            else return new CityDataDTO("success", result, "available");
        }

        public async Task<CityUpdateDataDTO> UpdateCity(CityUpdateDTO cityUpdateDTO)
        {
            try
            {
                var existingCity = await context.Cities.FindAsync(cityUpdateDTO.Id);
                if (existingCity == null)
                {
                    return new CityUpdateDataDTO("city is not available", null, "fail");
                }
                else
                {
                    existingCity.CityName = cityUpdateDTO.CityName;
                    existingCity.IsDeleted = cityUpdateDTO.IsDeleted;
                    context.Cities.Update(existingCity);
                    await context.SaveChangesAsync();
                    return new CityUpdateDataDTO("update success", cityUpdateDTO, "success");
                }
            }
            catch (Exception e)
            {
                return new CityUpdateDataDTO("update fail", null, "fail");
            }
        }

    }
}
