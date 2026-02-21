
using Backend.Data;
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
    }
}