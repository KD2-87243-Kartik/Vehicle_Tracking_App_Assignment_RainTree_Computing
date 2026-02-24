using Backend.DTO;
using Backend.DTOs;
using Backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace Backend.Controllers
{
    [Authorize]
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
            var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value
                              ?? User.FindFirst("nameid")?.Value;

            if (userIdClaim == null) return Unauthorized();

            var result = await _vehicleService.AddVehicle(vehicleDto, int.Parse(userIdClaim));
            return Ok(result);
        }

        //[HttpGet]
        //public async Task<IActionResult> GetAll()
        //{
        //    var vehicles = await _vehicleService.GetAllVehicles();
        //    return Ok(vehicles);
        //}

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var vehicle = await _vehicleService.GetVehicleById(id);

            if (vehicle == null)
                return NotFound("Vehicle not found");

            return Ok(vehicle);
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetMyUser(int userId)
        {
            var vehicles = await _vehicleService.GetMyVehicles(userId);
            return Ok(vehicles);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateVehicle(int id, VehicleUpdateDTO vehicleDto)
        {
            var userId = int.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier).Value);

            var updatedVehicle = await _vehicleService.UpdateVehicle(id, vehicleDto, userId);

            if (updatedVehicle == null)
                return NotFound();

            return Ok(updatedVehicle);
        }

        [HttpGet("my-fleet")]
        public async Task<IActionResult> GetMyFleet()
        {
            var userId = int.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier).Value);

            var vehicles = await _vehicleService.GetMyVehicles(userId);
            return Ok(vehicles);
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchVehicles(string? searchTerm, int pageNumber = 1, int pageSize = 5)
        {
            var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
            if (userIdClaim == null) return Unauthorized();

            int userId = int.Parse(userIdClaim.Value);
            Console.WriteLine($"---> API is searching for UserID: {userId}");
            var result = await _vehicleService.SearchVehicles(userId, searchTerm, pageNumber, pageSize);
            return Ok(result);
        }
    }
}