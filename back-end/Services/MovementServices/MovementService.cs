using AutoMapper;
using back_end.DTOs.MovementDTOs;
using back_end.Models;
using back_end.Repositories.MovementRepositories;

namespace back_end.Services.MovementServices;

public class MovementService(IMapper mapper, IMovementRepository movementRepository) : IMovementService
{
    public async Task<MovementDto> CreateMovementAsync(CreateMovementDto createMovementDto)
    {
        // Mapeia createMovementDto para Movement e atribui para movement
        var movement = mapper.Map<Movement>(createMovementDto);

        // Cria a lista de MovementProducts a partir do DTO
        movement.MovementProducts = createMovementDto.Products.Select(p => new MovementProduct
        {
            ProductId = p.ProductId,
            Quantity = p.Quantity
        }).ToList();

        // Chama o metodo createMovementAsync
        await movementRepository.CreateMovementAsync(movement);

        // Mapeia movement pra MovementDto
        return mapper.Map<MovementDto>(movement);
    }
}