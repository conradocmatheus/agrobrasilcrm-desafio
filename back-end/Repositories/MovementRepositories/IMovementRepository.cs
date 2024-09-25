using back_end.Models;
using back_end.Models.Enums;

namespace back_end.Repositories.MovementRepositories;

public interface IMovementRepository
{
    Task<Movement> CreateMovementAsync(Movement movement);
    Task<List<Movement>> GetAllMovementsAsync();
    Task<List<Movement>> GetAllMovementsByPaymentTypeAsync(PaymentType paymentType);
}