using back_end.Models;

namespace back_end.Repositories;

public interface IUserRepository
{
    Task AddUserAsync(User user);
}