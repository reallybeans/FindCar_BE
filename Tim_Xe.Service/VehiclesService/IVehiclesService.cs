using System.Threading.Tasks;
using Tim_Xe.Data.Models;

namespace Tim_Xe.Service.VehiclesService
{
    public interface IVehiclesService
    {
        Task<VehiclesDataDTO> GetAllVehicleByIdManager(int idManager);
        Task<VehiclesDataDTO> GetAllVehicle();
        Task<VehiclesCreateDataDTO> AddVehicle(VehicleCreateDTO vehicleCreateDTO);
        Task<VehiclesUpdateDataDTO> EditVehicle(VehiclesUpdateDTO vehiclesUpdateDTO);
        Task<VehicleUpdateStatusDataDTO> UpdateStatusVehiclesAsync(int id, string status);
        Task<bool> DeleteAsync(int id);
        //Task<VehiclesDataDTO> DeleteVehicle();
    }
}
