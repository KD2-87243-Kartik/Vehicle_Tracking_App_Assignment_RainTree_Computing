using Backend.DTOs;

namespace Backend.Services
{
    public interface IVehicleService
    {
        Task<VehicleResponseDto> AddVehicle(VehicleCreateDto vehicleDTO);
        Task<IEnumerable<VehicleResponseDto>> GetAllVehicles();
        Task<VehicleResponseDto> GetVehicleById(int id);
        Task<IEnumerable<VehicleResponseDto>> GetVehiclesByUserId(int userId);
    }
}