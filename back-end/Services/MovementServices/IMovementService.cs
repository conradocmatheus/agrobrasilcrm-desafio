using back_end.DTOs.MovementDTOs;
using back_end.Models;

namespace back_end.Services.MovementServices;

public interface IMovementService
{
    Task<MovementDto> CreateMovementAsync(CreateMovementDto createMovementDto);
    Task<List<Movement>> GetAllMovementsAsync();
}