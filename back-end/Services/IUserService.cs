using back_end.DTOs;

namespace back_end.Services;

public interface IUserService
{
    Task<UserDto> CreateUserAsync(CreateUserDto createUserDto);
    Task<List<UserCreatedAtDto>> GetUsersByCreatedAtAsync();
    Task<UserDto?> GetUserByIdAsync(Guid id);
    Task<UserDto?> DeleteUserByIdAsync(Guid id);
    Task<UserDto?> UpdateUserAsync(CreateUserDto createUserDto, Guid id);
}