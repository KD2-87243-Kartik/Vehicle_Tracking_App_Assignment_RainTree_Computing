using Backend.Data;
using Backend.DTO;
using Backend.DTOs;
using Backend.Models;
using Microsoft.EntityFrameworkCore;
using BCrypt.Net;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Backend.Services
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;

        public UserService(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<UserResponseDto> RegisterUser(UserCreateDto userDto)
        {
            var user = new User
            {
                Name = userDto.Name,
                Password = BCrypt.Net.BCrypt.HashPassword(userDto.Password),
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

        public async Task<IEnumerable<UserListDTO>> GetAllUsers()
        {
            return await _context.Users
                .Select(user => new UserListDTO
                {
                    UserID = user.UserID,
                    Name = user.Name,
                    emailAddress = user.EmailAddress,
                    MobileNumber = user.MobileNumber,
                    Organization = user.Organization
                })
                .ToListAsync();
        }

        public async Task<UserDetailsDTO> UpdateUser(int id, UserUpdateDTO userDto)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
                return null;

            user.Name = userDto.Name;
            user.MobileNumber = userDto.MobileNumber;
            user.Organization = userDto.Organization;
            user.Address = userDto.Address;
            user.EmailAddress = userDto.EmailAddress;
            user.Location = userDto.Location;
            if (userDto.PhotoPath != null)
            {
                user.PhotoPath = userDto.PhotoPath;
            }

            await _context.SaveChangesAsync();

            return new UserDetailsDTO
            {
                UserID = user.UserID,
                Name = user.Name,
                MobileNumber = user.MobileNumber,
                Organization = user.Organization,
                Address = user.Address,
                EmailAddress = user.EmailAddress,
                Location = user.Location,
                PhotoPath = user.PhotoPath
            };
        }

        public async Task<string?> Login(UserLoginDto loginDto)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.EmailAddress == loginDto.EmailAddress);

            if (user == null)
                return null;

            bool isValidPassword = BCrypt.Net.BCrypt
                .Verify(loginDto.Password, user.Password);

            if (!isValidPassword)
                return null;

            var claims = new[]
            {
                new Claim(ClaimTypes.Email, user.EmailAddress),
                new Claim(ClaimTypes.NameIdentifier, user.UserID.ToString())
            };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(
                    Convert.ToDouble(_configuration["Jwt:DurationInMinutes"])),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}