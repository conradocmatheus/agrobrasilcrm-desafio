using AutoMapper;
using back_end.DTOs.MovementDTOs;
using back_end.Helpers;
using back_end.Models;
using back_end.Models.Enums;
using back_end.Repositories.MovementRepositories;

namespace back_end.Services.MovementServices;

public class MovementService(IMapper mapper, IMovementRepository movementRepository) : IMovementService
{
    // Criar movimentação
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

        // Chama o método createMovementAsync
        await movementRepository.CreateMovementAsync(movement);

        // Mapeia movement pra MovementDto
        return mapper.Map<MovementDto>(movement);
    }

    // Listar movimentações
    public async Task<List<GetAllMovementsWithUserInfoDto>> GetAllMovementsAsync(QueryObject query)
    {
        // Atribui a lista que o método do repository retorna em uma var movements
        var movements = await movementRepository.GetAllMovementsAsync(query);
        // Retorna a lista com os objetos mapeados para DTO
        return mapper.Map<List<GetAllMovementsWithUserInfoDto>>(movements);
    }

    // Lista as movimentações por tipo de pagamento
    public async Task<List<GetAllMovementsDto>> GetAllMovementsByPaymentTypeAsync(PaymentType paymentType)
    {
        var movements = await movementRepository.GetAllMovementsByPaymentTypeAsync(paymentType);
        return mapper.Map<List<GetAllMovementsDto>>(movements);
    }
}