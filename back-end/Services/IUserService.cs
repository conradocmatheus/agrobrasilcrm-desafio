using back_end.DTOs;

namespace back_end.Services;

public interface IUserService
{
    Task CreateUserAsync(CreateUserDto createUserDto);
}