using Backend.Helpers;
using Backend.Data;
using Backend.DTO;
using Backend.DTOs;
using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Services
{
    public class VehicleService : IVehicleService
    {
        private readonly AppDbContext _context;

        public VehicleService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<VehicleResponseDto> AddVehicle(VehicleCreateDto vehicleDTO)
        {
            var userExists = await _context.Users
                .AnyAsync(u => u.UserID == vehicleDTO.UserID);

            if (!userExists) return null!;

            var vehicle = new Vehicle
            {
                VehicleNumber = vehicleDTO.VehicleNumber,
                VehicleType = vehicleDTO.VehicleType,
                ChassisNumber = vehicleDTO.ChassisNumber,
                EngineNumber = vehicleDTO.EngineNumber,
                ManufacturingYear = vehicleDTO.ManufacturingYear,
                LoadCarryingCapacity = vehicleDTO.LoadCarryingCapacity,
                MakeOfVehicle = vehicleDTO.MakeOfVehicle,
                ModelNumber = vehicleDTO.ModelNumber,
                BodyType = vehicleDTO.BodyType,
                OrganisationName = vehicleDTO.OrganisationName,
                DeviceID = vehicleDTO.DeviceID,
                UserID = vehicleDTO.UserID
            };

            _context.Vehicles.Add(vehicle);
            await _context.SaveChangesAsync();

            return MapToResponseDto(vehicle);
        }

        //public async Task<IEnumerable<VehicleResponseDto>> GetAllVehicles()
        //{
        //    return await _context.Vehicles
        //        .Select(vehicle => new VehicleResponseDto
        //        {
        //            VehicleID = vehicle.VehicleID,
        //            UserID = vehicle.UserID,
        //            VehicleNumber = vehicle.VehicleNumber,
        //            VehicleType = vehicle.VehicleType,
        //            ManufacturingYear = vehicle.ManufacturingYear,
        //            ModelNumber = vehicle.ModelNumber
        //        })
        //        .ToListAsync();
        //}

        public async Task<VehicleResponseDto> GetVehicleById(int id)
        {
            var vehicle = await _context.Vehicles.FindAsync(id);
            return vehicle == null ? null! : MapToResponseDto(vehicle);
        }

        public async Task<IEnumerable<VehicleResponseDto>> GetMyVehicles(int userId)
        {
            return await _context.Vehicles
                .Where(v => v.UserID == userId)
                .Select(v => MapToResponseDto(v))
                .ToListAsync();
        }

        public async Task<VehicleResponseDto?> UpdateVehicle(int id, VehicleUpdateDTO vehicleDto, int userId)
        {
            var vehicle = await _context.Vehicles.FindAsync(id);

            if (vehicle == null || vehicle.UserID != userId)
                return null;

            vehicle.VehicleNumber = vehicleDto.VehicleNumber ?? vehicle.VehicleNumber;
            vehicle.VehicleType = vehicleDto.VehicleType ?? vehicle.VehicleType;
            vehicle.ChassisNumber = vehicleDto.ChassisNumber ?? vehicle.ChassisNumber;
            vehicle.EngineNumber = vehicleDto.EngineNumber ?? vehicle.EngineNumber;
            vehicle.MakeOfVehicle = vehicleDto.MakeOfVehicle ?? vehicle.MakeOfVehicle;
            vehicle.ModelNumber = vehicleDto.ModelNumber ?? vehicle.ModelNumber;
            vehicle.BodyType = vehicleDto.BodyType ?? vehicle.BodyType;
            vehicle.OrganisationName = vehicleDto.OrganisationName ?? vehicle.OrganisationName;
            vehicle.DeviceID = vehicleDto.DeviceID ?? vehicle.DeviceID;

            if (vehicleDto.ManufacturingYear.HasValue)
                vehicle.ManufacturingYear = vehicleDto.ManufacturingYear.Value;

            if (vehicleDto.LoadCarryingCapacity.HasValue)
                vehicle.LoadCarryingCapacity = vehicleDto.LoadCarryingCapacity.Value;

            await _context.SaveChangesAsync();

            return MapToResponseDto(vehicle);
        }


        public async Task<PagedResult<VehicleResponseDto>> SearchVehicles(int userId, string? searchTerm, int pageNumber, int pageSize)
        {
            var query = _context.Vehicles.Where(v => v.UserID == userId).AsQueryable();

            // Wild search
            if (!string.IsNullOrEmpty(searchTerm))
            {
                query = query.Where(v =>
                    v.VehicleNumber.Contains(searchTerm) ||
                    v.VehicleType.Contains(searchTerm) ||
                    v.ModelNumber.Contains(searchTerm) ||
                    v.OrganisationName.Contains(searchTerm));
            }

            var totalRecords = await query.CountAsync();

            var vehicles = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(v => new VehicleResponseDto
                {
                    VehicleID = v.VehicleID,
                    UserID = v.UserID,
                    VehicleNumber = v.VehicleNumber,
                    VehicleType = v.VehicleType,
                    ManufacturingYear = v.ManufacturingYear,
                    ModelNumber = v.ModelNumber
                })
                .ToListAsync();

            return new PagedResult<VehicleResponseDto>
            {
                TotalRecords = totalRecords,
                PageNumber = pageNumber,
                PageSize = pageSize,
                Data = vehicles
            };
        }

        private VehicleResponseDto MapToResponseDto(Vehicle v)
        {
            return new VehicleResponseDto
            {
                VehicleID = v.VehicleID,
                UserID = v.UserID,
                VehicleNumber = v.VehicleNumber,
                VehicleType = v.VehicleType,
                ManufacturingYear = v.ManufacturingYear,
                ModelNumber = v.ModelNumber,
                ChassisNumber = v.ChassisNumber,
                EngineNumber = v.EngineNumber,
                LoadCarryingCapacity = v.LoadCarryingCapacity,
                MakeOfVehicle = v.MakeOfVehicle,
                BodyType = v.BodyType,
                OrganisationName = v.OrganisationName,
                DeviceID = v.DeviceID
            };
        }

    }
}