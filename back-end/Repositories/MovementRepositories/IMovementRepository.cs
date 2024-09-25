using back_end.Models;

namespace back_end.Repositories.MovementRepositories;

public interface IMovementRepository
{
    Task<Movement> CreateMovementAsync(Movement movement);
    Task<List<Movement>> GetAllMovementsAsync();
}