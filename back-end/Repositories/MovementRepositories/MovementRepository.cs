using back_end.Data;
using back_end.Models;
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
}