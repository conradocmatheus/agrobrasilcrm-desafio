using back_end.Data;
using back_end.Helpers;
using back_end.Models;
using back_end.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace back_end.Repositories.MovementRepositories;

public class MovementRepository(AppDbContext context) : IMovementRepository
{
    // Criar uma movimentação
    public async Task<Movement> CreateMovementAsync(Movement movement)
    {
        movement.Id = Guid.NewGuid();

        await context.Movements.AddAsync(movement);
        await context.SaveChangesAsync();

        return movement;
    }

    // Listar movimentações paginadas e mostrando info do user
    public async Task<List<Movement>> GetAllMovementsAsync(QueryObject query)
    {
        var skipNumber = (query.PageNumber - 1) * query.PageSize;
        
        return await context.Movements
            .Include(m => m.MovementProducts)
            .Include(m => m.User)
            .AsNoTracking()
            .Skip(skipNumber)
            .Take(query.PageSize)
            .ToListAsync();
    }

    // Listar todas no débito ou no crédito
    public async Task<List<Movement>> GetAllMovementsByPaymentTypeAsync(PaymentType paymentType)
    {
        return await context.Movements
            .Include(m => m.MovementProducts)
            .Where(movement => movement.PaymentType == paymentType)
            .ToListAsync();
    }
}