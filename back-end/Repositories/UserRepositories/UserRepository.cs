using back_end.Data;
using back_end.Models;
using Microsoft.EntityFrameworkCore;

namespace back_end.Repositories.UserRepositories;

public class UserRepository(AppDbContext context) : IUserRepository
{
    // Criar um novo usuário no banco
    public async Task<User> CreateUserAsync(User user)
    {
        user.Id = Guid.NewGuid();
        user.CreatedAt = DateTime.UtcNow;
        user.UpdatedAt = DateTime.UtcNow;
        
        await context.Users.AddAsync(user);
        await context.SaveChangesAsync();
        return user;
    }

    // Deleta um usuário no banco por ID
    public async Task<User?> DeleteUserByIdAsync(Guid id)
    {
        var existingUser = await context.Users.FirstOrDefaultAsync(x => x.Id == id);

        if (existingUser == null)
        {
            return null;
        }

        context.Users.Remove(existingUser);
        await context.SaveChangesAsync();
        return existingUser;
    }

    // Lista todos os usuários ordenados por data de criação
    public async Task<List<User>> GetUsersByCreatedAtAsync()
    {
        return await context.Users
            .OrderBy(user => user.CreatedAt)
            .AsNoTracking()
            .ToListAsync();
    }

    // Busca um usuário por ID
    public async Task<User?> GetUserByIdAsync(Guid id)
    {
        return await context.Users
            .Include(u => u.Movements)
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);
    }
}