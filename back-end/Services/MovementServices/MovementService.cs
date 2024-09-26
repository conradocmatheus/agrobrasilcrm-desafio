using AutoMapper;
using back_end.DTOs.MovementDTOs;
using back_end.Helpers;
using back_end.Models;
using back_end.Models.Enums;
using back_end.Repositories.MovementRepositories;

namespace back_end.Services.MovementServices;

public class MovementService(
    IMapper mapper,
    IMovementRepository movementRepository) : IMovementService
{
    // Criar movimentação
    public async Task<MovementDto> CreateMovementAsync(CreateMovementDto createMovementDto)
    {
        // Verifica se o usuário existe
        if (!await movementRepository.UserExistsAsync(createMovementDto.UserId))
        {
            throw new Exception("Usuário com esse id não encontrado.");
        }

        // Verifica se os ids de produto existem
        foreach (var product in createMovementDto.Products)
        {
            if (!await movementRepository.ProductExistsAsync(product.ProductId))
            {
                throw new Exception($"Produto com ID {product.ProductId} não encontrado.");
            }
        }

        // Cria a movimentação diretamente a partir do DTO
        var movement = new Movement
        {
            UserId = createMovementDto.UserId,
            MovementProducts = createMovementDto.Products.Select(p => new MovementProduct
            {
                ProductId = p.ProductId,
                Quantity = p.Quantity
            }).ToList()
        };

        // Chama o método para criar a movimentação
        await movementRepository.CreateMovementAsync(movement);

        // Retorna a movimentação criada para MovementDto
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