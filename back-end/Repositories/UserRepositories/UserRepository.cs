using back_end.Data;
using back_end.Models;
using Microsoft.EntityFrameworkCore;

namespace back_end.Repositories.UserRepositories;

public class UserRepository(AppDbContext context) : IUserRepository
{
    // Criar usuário no banco
    public async Task<User> CreateUserAsync(User user)
    {
        // Gera o id tipo GUID e as datas de criação e atualização em (UTC)
        user.Id = Guid.NewGuid();
        user.CreatedAt = DateTime.UtcNow;
        user.UpdatedAt = DateTime.UtcNow;

        // Adiciona o usuário no banco
        await context.Users.AddAsync(user);
        // Salva as alterações no banco
        await context.SaveChangesAsync();
        // Retorna o usuário criado, com id e datas préviamente preenchidas
        return user;
    }
    
    // Atualizar usuário no banco por ID
    public async Task<User?> UpdateUserAsync(User user, Guid id)
    {
        try
        {
            var toUpdateUser = await context.Users.FirstOrDefaultAsync(x => x.Id == id);

            if (toUpdateUser == null)
            {
                return null; // Retorna null se o usuário não for encontrado
            }

            // Atualiza as propriedades
            toUpdateUser.Name = user.Name;
            toUpdateUser.Email = user.Email;
            toUpdateUser.Birthday = user.Birthday;
            toUpdateUser.UpdatedAt = DateTime.Now;

            await context.SaveChangesAsync();
            return toUpdateUser; // Retorna o usuário atualizado
        }
        catch (DbUpdateException e)
        {
            throw new InvalidOperationException($"Não foi possível atualizar," +
                                                $" usuário com ID {id} não encontrado.", e);
        }
    }
    
    // Deletar usuário no banco por ID
    public async Task<User?> DeleteUserByIdAsync(Guid id)
    {
        try
        {
            // Pega o usuário com o id fornecido ou null se não encontrar
            var existingUser = await context.Users.FirstOrDefaultAsync(x => x.Id == id);

            if (existingUser == null)
            {
                return null;
            }

            // Remove o usuário do banco, salva e retorna o próprio usuário removido
            context.Users.Remove(existingUser);
            await context.SaveChangesAsync();
            return existingUser;
        }
        catch (DbUpdateException e)
        {
            throw new InvalidOperationException("Não foi possível deletar o usuário.", e);
        }
    }

    // Listar usuários de acordo com a data de criação
    public async Task<List<User>> GetUsersByCreatedAtAsync()
    {
        // Retorna o usuário pelo ID ou nulo se não for encontrado
        return await context.Users
            .OrderBy(user => user.CreatedAt)
            .AsNoTracking() // Melhora a performance
            .ToListAsync();
    }

    // Encontra usuário por ID
    public async Task<User?> GetUserByIdAsync(Guid id)
    {
        // Retorna o usuário pelo ID ou nulo se não for encontrado
        return await context.Users
            .AsNoTracking() // Melhora a performance
            .FirstOrDefaultAsync(x => x.Id == id);
    }
}