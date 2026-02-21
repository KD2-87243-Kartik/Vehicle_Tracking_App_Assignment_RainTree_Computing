using Backend.DTO;
using Backend.DTOs;
using Backend.Services;
using Microsoft.AspNetCore.Mvc;


namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehiclesController : ControllerBase
    {
        private readonly IVehicleService _vehicleService;

        public VehiclesController(IVehicleService vehicleService)
        {
            _vehicleService = vehicleService;
        }

        [HttpPost]
        public async Task<IActionResult> AddVehicle(VehicleCreateDto vehicleDto)
        {
            var result = await _vehicleService.AddVehicle(vehicleDto);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var vehicles = await _vehicleService.GetAllVehicles();
            return Ok(vehicles);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var vehicle = await _vehicleService.GetVehicleById(id);

            if (vehicle == null)
                return NotFound("Vehicle not found");

            return Ok(vehicle);
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetByUser(int userId)
        {
            var vehicles = await _vehicleService.GetVehiclesByUserId(userId);
            return Ok(vehicles);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateVehicle(int id, VehicleUpdateDTO vehicleDto)
        {
            var updatedVehicle = await _vehicleService.UpdateVehicle(id, vehicleDto);

            if (updatedVehicle == null)
                return NotFound();

            return Ok(updatedVehicle);
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchVehicles(string? searchTerm, int pageNumber = 1, int pageSize = 5)
        {
            var result = await _vehicleService
                .SearchVehicles(searchTerm, pageNumber, pageSize);

            return Ok(result);
        }
    }
}