using back_end.Helpers;
using back_end.Models;
using back_end.Models.Enums;

namespace back_end.Repositories.MovementRepositories;

public interface IMovementRepository
{
    Task<Movement> CreateMovementAsync(Movement movement);
    Task<List<Movement>> GetAllMovementsAsync(QueryObject query);
    Task<List<Movement>> GetAllMovementsByPaymentTypeAsync(PaymentType paymentType);

    Task<bool> UserExistsAsync(Guid id);
    Task<bool> ProductExistsAsync(Guid id);
}