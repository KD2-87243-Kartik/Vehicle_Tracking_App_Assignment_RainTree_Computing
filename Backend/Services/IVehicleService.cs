using Backend.DTO;
using Backend.DTOs;
using Backend.Helpers;

namespace Backend.Services
{
    public interface IVehicleService
    {
        Task<VehicleResponseDto> AddVehicle(VehicleCreateDto vehicleDTO);
        Task<IEnumerable<VehicleResponseDto>> GetAllVehicles();
        Task<VehicleResponseDto> GetVehicleById(int id);
        Task<IEnumerable<VehicleResponseDto>> GetVehiclesByUserId(int userId);
        Task<VehicleResponseDto?> UpdateVehicle(int id, VehicleUpdateDTO vehicleDto);
        Task<PagedResult<VehicleResponseDto>> SearchVehicles(string? searchTerm, int pageNumber, int pageSize);
    }
}