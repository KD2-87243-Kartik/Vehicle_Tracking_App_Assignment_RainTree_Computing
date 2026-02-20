using Backend.Data;
using Backend.DTOs;
using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Services
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _context;

        public UserService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<UserResponseDto> RegisterUser(UserCreateDto userDto)
        {
            var user = new User
            {
                Name = userDto.Name,
                Password = userDto.Password,
                MobileNumber = userDto.MobileNumber,
                Organization = userDto.Organization,
                Address = userDto.Address,
                EmailAddress = userDto.EmailAddress,
                Location = userDto.Location
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return new UserResponseDto
            {
                UserID = user.UserID,
                Name = user.Name,
                MobileNumber = user.MobileNumber,
                Organization = user.Organization,
                EmailAddress = user.EmailAddress,
                Location = user.Location
            };
        }

        public async Task<UserResponseDto> GetUserById(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null) return null;

            return new UserResponseDto
            {
                UserID = user.UserID,
                Name = user.Name,
                MobileNumber = user.MobileNumber,
                Organization = user.Organization,
                EmailAddress = user.EmailAddress,
                Location = user.Location
            };
        }

        public async Task<IEnumerable<UserResponseDto>> GetAllUsers()
        {
            return await _context.Users
                .Select(user => new UserResponseDto
                {
                    UserID = user.UserID,
                    Name = user.Name,
                    MobileNumber = user.MobileNumber,
                    Organization = user.Organization,
                    EmailAddress = user.EmailAddress,
                    Location = user.Location
                })
                .ToListAsync();
        }
    }
}