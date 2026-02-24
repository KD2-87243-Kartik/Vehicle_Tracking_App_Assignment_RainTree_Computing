using Backend.DTO;
using Backend.DTOs;
using Backend.Helpers;

namespace Backend.Services
{
    public interface IVehicleService
    {
        Task<VehicleResponseDto> AddVehicle(VehicleCreateDto vehicleDTO);

        Task<IEnumerable<VehicleResponseDto>> GetMyVehicles(int userId);

        Task<VehicleResponseDto> GetVehicleById(int id);

        Task<VehicleResponseDto?> UpdateVehicle(int id, VehicleUpdateDTO vehicleDto, int userId);

        Task<PagedResult<VehicleResponseDto>> SearchVehicles(int userId, string? searchTerm, int pageNumber = 1, int pageSize = 5);
    }
}