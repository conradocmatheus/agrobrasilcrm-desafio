using back_end.Models;

namespace back_end.Repositories;

public interface IUserRepository
{
    Task CreateUserAsync(User user);
    Task<List<User>> GetUsersByCreatedAtAsync();
    Task<User?> GetUserByIdAsync(Guid id);
    Task<User?> DeleteUserByIdAsync(Guid id);
    Task<User?> UpdateUserAsync(User user, Guid id);
}