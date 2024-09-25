using back_end.DTOs.MovementDTOs;
using back_end.Models;
using back_end.Models.Enums;

namespace back_end.Services.MovementServices;

public interface IMovementService
{
    Task<MovementDto> CreateMovementAsync(CreateMovementDto createMovementDto);
    Task<List<GetAllMovementsDto>> GetAllMovementsAsync();
    Task<List<GetAllMovementsDto>> GetAllMovementsByPaymentTypeAsync(PaymentType paymentType);
}