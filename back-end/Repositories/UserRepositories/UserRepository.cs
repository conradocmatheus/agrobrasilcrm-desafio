using back_end.Data;
using back_end.Models;
using Microsoft.EntityFrameworkCore;

namespace back_end.Repositories.UserRepositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<User> CreateUserAsync(User user)
    {
        // Gera o id tipo GUID e as datas de criação e atualização em (UTC)
        user.Id = Guid.NewGuid();
        user.CreatedAt = DateTime.UtcNow;
        user.UpdatedAt = DateTime.UtcNow;

        // Adiciona o usuário no banco
        await _context.Users.AddAsync(user);
        // Salva as alterações no banco
        await _context.SaveChangesAsync();
        // Retorna o usuário criado, com id e datas préviamente preenchidas
        return user;
    }

    public async Task<List<User>> GetUsersByCreatedAtAsync()
    {
        // Retorna o usuário pelo ID ou nulo se não for encontrado
        return await _context.Users
            .OrderBy(user => user.CreatedAt)
            .AsNoTracking() // Melhora a performance
            .ToListAsync();
    }

    public async Task<User?> GetUserByIdAsync(Guid id)
    {
        // Retorna o usuário pelo ID ou nulo se não for encontrado
        return await _context.Users
            .AsNoTracking() // Melhora a performance
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<User?> DeleteUserByIdAsync(Guid id)
    {
        // Pega o usuário com o id fornecido ou null se não encontrar
        var existingUser = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);

        if (existingUser == null)
        {
            return null;
        }

        // Remove o usuário do banco, salva e retorna o próprio
        _context.Users.Remove(existingUser);
        await _context.SaveChangesAsync();
        return existingUser;
    }

    public async Task<User?> UpdateUserAsync(User user, Guid id)
    {
        var toUpdateUser = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
        if (toUpdateUser == null)
        {
            return null;
        }

        toUpdateUser.Name = user.Name;
        toUpdateUser.Email = user.Email;
        toUpdateUser.Birthday = user.Birthday;
        toUpdateUser.UpdatedAt = DateTime.Now;

        await _context.SaveChangesAsync();
        return toUpdateUser;
    }
}