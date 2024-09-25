using back_end.Data;
using back_end.Models;
using back_end.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace back_end.Repositories.MovementRepositories;

public class MovementRepository(AppDbContext context) : IMovementRepository
{
    public async Task<Movement> CreateMovementAsync(Movement movement)
    {
        movement.Id = Guid.NewGuid();

        await context.Movements.AddAsync(movement);
        await context.SaveChangesAsync();

        return movement;
    }

    public async Task<List<Movement>> GetAllMovementsAsync()
    {
        return await context.Movements
            .Include(m => m.MovementProducts)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<List<Movement>> GetAllMovementsByPaymentTypeAsync(PaymentType paymentType)
    {
        return await context.Movements
            .Where(movement => movement.PaymentType == paymentType)
            .ToListAsync();
    }
}