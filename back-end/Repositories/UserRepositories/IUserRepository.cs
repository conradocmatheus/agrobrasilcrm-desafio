using back_end.Models;

namespace back_end.Repositories.UserRepositories;

public interface IUserRepository
{
    Task<User> CreateUserAsync(User user);
    Task<List<User>> GetUsersByCreatedAtAsync();
    Task<User?> GetUserByIdAsync(Guid id);
    Task<User?> DeleteUserByIdAsync(Guid id);
    Task<User?> UpdateUserAsync(User user, Guid id);
}