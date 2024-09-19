using back_end.Data;
using back_end.Models;

using Microsoft.EntityFrameworkCore;

namespace back_end.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddUserAsync(User user)
    {
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
    }

    public async Task<List<User>> GetUsersByCreatedAtAsync()
    {
        return await _context.Users.OrderBy(user => user.CreatedAt).ToListAsync();
    }

    public async Task<User?> GetUserByIdAsync(Guid id)
    {
        return await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
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
}