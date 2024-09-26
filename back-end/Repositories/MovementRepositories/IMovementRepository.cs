using back_end.Helpers;
using back_end.Models;
using back_end.Models.Enums;

namespace back_end.Repositories.MovementRepositories;

public interface IMovementRepository
{
    Task<Movement> CreateMovementAsync(Movement movement);
    Task<List<Movement>> GetAllMovementsPaginatedAsync(QueryObject query);
    Task<List<Movement>> GetAllMovementsByPaymentTypeAsync(PaymentType paymentType);
    Task<Movement?> DeleteMovementByIdAsync(Guid id);
    Task<Movement?> GetMovementByIdAsync(Guid id);

    Task<double> GetProductPriceAsync(Guid productId);
    Task<bool> UserExistsAsync(Guid id);
    Task<bool> ProductExistsAsync(Guid id);
    
    Task<List<Movement>> GetMovementsLast30DaysAsync();
    Task<List<Movement>> GetMovementsByMonthYearAsync(int month, int year);
    Task<List<Movement>> GetAllMovementsAsync();
}