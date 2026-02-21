using Backend.DTO;
using Backend.DTOs;

namespace Backend.Services
{
    public interface IUserService
    {
        Task<UserResponseDto> RegisterUser(UserCreateDto userDTO);
        Task<UserResponseDto> GetUserById(int id);
        Task<IEnumerable<UserListDTO>> GetAllUsers();       
        Task<UserDetailsDTO> UpdateUser(int id, UserUpdateDTO userDto);
    }
}
