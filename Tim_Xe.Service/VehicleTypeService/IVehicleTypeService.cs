using System.Threading.Tasks;
using Tim_Xe.Data.Models;

namespace Tim_Xe.Service.VehicleTypeService
{
    public interface IVehicleTypeService
    {
        Task<VehicleTypeListDataDTO> GetAllVehicleTypesAsync();
        Task<VehicleTypeDTO> GetVehicleTypeByIdAsync(int id);
        Task<int> CreateVehicleType(VehicleTypeCreateDTO vehicleType);
        Task<int> UpdateVehicleType(VehicleTypeUpdateDTO vehicleType);
        Task<bool> DeleteVehicleTypeAsync(int id);

    }
}
