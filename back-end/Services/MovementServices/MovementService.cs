using AutoMapper;
using back_end.DTOs.MovementDTOs;
using back_end.Models;
using back_end.Repositories.MovementRepositories;

namespace back_end.Services.MovementServices;

public class MovementService(IMapper mapper, IMovementRepository movementRepository) : IMovementService
{
    public async Task<MovementDto> CreateMovementAsync(CreateMovementDto createMovementDto)
    {
        var movement = mapper.Map<Movement>(createMovementDto);

        movement.MovementProducts = createMovementDto.Products.Select(p => new MovementProduct
        {
            ProductId = p.ProductId,
            Quantity = p.Quantity
        }).ToList();

        try
        {
            await movementRepository.CreateMovementAsync(movement);
        }
        catch (Exception e)
        {
            throw new InvalidOperationException("Erro ao criar movimentação.", e);
        }

        return mapper.Map<MovementDto>(movement);
    }
}