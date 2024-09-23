using back_end.DTOs;
using back_end.DTOs.UserDTOs;

namespace back_end.Services.UserServices;

public interface IUserService
{
    Task<UserDto> CreateUserAsync(CreateUserDto createUserDto);
    Task<List<UserCreatedAtDto>> GetUsersByCreatedAtAsync();
    Task<UserDto?> GetUserByIdAsync(Guid id);
    Task<UserDto?> DeleteUserByIdAsync(Guid id);
    Task<UserDto?> UpdateUserAsync(CreateUserDto createUserDto, Guid id);
}