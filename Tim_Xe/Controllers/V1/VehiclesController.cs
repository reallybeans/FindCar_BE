using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Tim_Xe.Data.Models;
using Tim_Xe.Service.VehiclesService;

namespace Tim_Xe.API.Controllers.V1
{
    [Route("api/v1/vehicles")]
    [ApiController]
    public class VehiclesController : ControllerBase
    {
        private readonly VehiclesServiceImp _vehiclesServiceImp;
        public VehiclesController()
        {
            _vehiclesServiceImp = new VehiclesServiceImp();
        }
        [HttpGet]
        public async Task<VehiclesDataDTO> GetAll()
        {
            return await _vehiclesServiceImp.GetAllVehicle();
        }
        [HttpGet("{id}")]
        public async Task<VehiclesDataDTO> GetAllByIdManager(int id)
        {
            return await _vehiclesServiceImp.GetAllVehicleByIdManager(id);
        }
        [HttpPost]
        public async Task<VehiclesCreateDataDTO> AddVehicle(VehicleCreateDTO vehicleCreateDTO)
        {
            return await _vehiclesServiceImp.AddVehicle(vehicleCreateDTO);
        }
        [HttpPut]
        public async Task<VehiclesUpdateDataDTO> EditVehicle(VehiclesUpdateDTO vehiclesUpdateDTO)
        {
            return await _vehiclesServiceImp.EditVehicle(vehiclesUpdateDTO);
        }
        [HttpPut("update-status")]
        public async Task<VehicleUpdateStatusDataDTO> UpdateStatus(int id, string status)
        {
            return await _vehiclesServiceImp.UpdateStatusVehiclesAsync(id, status);
        }
        [HttpDelete("{id}")]
        public async Task<bool> DeleteAsync(int id)
        {
            return await _vehiclesServiceImp.DeleteAsync(id);
        }
        [HttpGet("get-by-driver-id/{id}")]
        public async Task<VehiclesDataDTO> getVehiclesByDriverId(int id)
        {
            return await _vehiclesServiceImp.getVehiclesByDriverId(id);
        }
    }
}
