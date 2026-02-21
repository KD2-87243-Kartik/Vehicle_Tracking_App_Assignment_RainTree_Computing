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

            if (!userExists)
                return null;

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

            return new VehicleResponseDto
            {
                VehicleID = vehicle.VehicleID,
                UserID = vehicle.UserID,
                VehicleNumber = vehicle.VehicleNumber,
                VehicleType = vehicle.VehicleType,
                ManufacturingYear = vehicle.ManufacturingYear,
                ModelNumber = vehicle.ModelNumber
            };
        }

        public async Task<IEnumerable<VehicleResponseDto>> GetAllVehicles()
        {
            return await _context.Vehicles
                .Select(vehicle => new VehicleResponseDto
                {
                    VehicleID = vehicle.VehicleID,
                    UserID = vehicle.UserID,
                    VehicleNumber = vehicle.VehicleNumber,
                    VehicleType = vehicle.VehicleType,
                    ManufacturingYear = vehicle.ManufacturingYear,
                    ModelNumber = vehicle.ModelNumber
                })
                .ToListAsync();
        }

        public async Task<VehicleResponseDto> GetVehicleById(int id)
        {
            var vehicle = await _context.Vehicles.FindAsync(id);

            if (vehicle == null) return null;

            return new VehicleResponseDto
            {
                VehicleID = vehicle.VehicleID,
                UserID = vehicle.UserID,
                VehicleNumber = vehicle.VehicleNumber,
                VehicleType = vehicle.VehicleType,
                ManufacturingYear = vehicle.ManufacturingYear,
                ModelNumber = vehicle.ModelNumber
            };
        }

        public async Task<IEnumerable<VehicleResponseDto>> GetVehiclesByUserId(int userId)
        {
            return await _context.Vehicles
                .Where(v => v.UserID == userId)
                .Select(vehicle => new VehicleResponseDto
                {
                    VehicleID = vehicle.VehicleID,
                    UserID = vehicle.UserID,
                    VehicleNumber = vehicle.VehicleNumber,
                    VehicleType = vehicle.VehicleType,
                    ManufacturingYear = vehicle.ManufacturingYear,
                    ModelNumber = vehicle.ModelNumber
                })
                .ToListAsync();
        }

        public async Task<VehicleResponseDto?> UpdateVehicle(int id, VehicleUpdateDTO vehicleDto)
        {
            var vehicle = await _context.Vehicles.FindAsync(id);

            if (vehicle == null)
                return null;

            if (vehicleDto.VehicleNumber != null)
                vehicle.VehicleNumber = vehicleDto.VehicleNumber;

            if (vehicleDto.VehicleType != null)
                vehicle.VehicleType = vehicleDto.VehicleType;

            if (vehicleDto.ChassisNumber != null)
                vehicle.ChassisNumber = vehicleDto.ChassisNumber;

            if (vehicleDto.EngineNumber != null)
                vehicle.EngineNumber = vehicleDto.EngineNumber;

            if (vehicleDto.ManufacturingYear.HasValue)
                vehicle.ManufacturingYear = vehicleDto.ManufacturingYear.Value;

            if (vehicleDto.LoadCarryingCapacity.HasValue)
                vehicle.LoadCarryingCapacity = vehicleDto.LoadCarryingCapacity.Value;

            if (vehicleDto.MakeOfVehicle != null)
                vehicle.MakeOfVehicle = vehicleDto.MakeOfVehicle;

            if (vehicleDto.ModelNumber != null)
                vehicle.ModelNumber = vehicleDto.ModelNumber;

            if (vehicleDto.BodyType != null)
                vehicle.BodyType = vehicleDto.BodyType;

            if (vehicleDto.OrganisationName != null)
                vehicle.OrganisationName = vehicleDto.OrganisationName;

            if (vehicleDto.DeviceID != null)
                vehicle.DeviceID = vehicleDto.DeviceID;

            await _context.SaveChangesAsync();

            return new VehicleResponseDto
            {
                VehicleID = vehicle.VehicleID,
                UserID = vehicle.UserID,
                VehicleNumber = vehicle.VehicleNumber,
                VehicleType = vehicle.VehicleType,
                ManufacturingYear = vehicle.ManufacturingYear,
                ModelNumber = vehicle.ModelNumber
            };
        }

        public async Task<PagedResult<VehicleResponseDto>> SearchVehicles(string? searchTerm, int pageNumber, int pageSize)
        {
            var query = _context.Vehicles.AsQueryable();

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
    }
}