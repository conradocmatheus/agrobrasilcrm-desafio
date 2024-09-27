using System.Globalization;
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
        if (!await movementRepository.UserExistsAsync(createMovementDto.UserId))
        {
            throw new Exception("Usuário com esse id não encontrado.");
        }

        double totalValue = 0;
        
        // Calcula o valor total e verifica a existência dos produtos
        foreach (var product in createMovementDto.Products)
        {
            if (!await movementRepository.ProductExistsAsync(product.ProductId))
            {
                throw new Exception($"Produto com ID {product.ProductId} não encontrado.");
            }

            var productPrice = await movementRepository.GetProductPriceAsync(product.ProductId);
            totalValue += productPrice * product.Quantity;
        }

        // Cria a movimentação diretamente a partir do DTO
        var movement = new Movement
        {
            UserId = createMovementDto.UserId,
            TotalValue = totalValue,
            MovementProducts = createMovementDto.Products.Select(p => new MovementProduct
            {
                ProductId = p.ProductId,
                Quantity = p.Quantity
            }).ToList()
        };
        
        await movementRepository.CreateMovementAsync(movement);
        return mapper.Map<MovementDto>(movement);
    }

    // Listar movimentações
    public async Task<List<GetAllMovementsWithUserInfoDto>> GetAllMovementsPaginatedAsync(QueryObject query)
    {
        var movements = await movementRepository.GetAllMovementsPaginatedAsync(query);
        return mapper.Map<List<GetAllMovementsWithUserInfoDto>>(movements);
    }

    // Lista as movimentações por tipo de pagamento
    public async Task<List<GetAllMovementsDto>> GetAllMovementsByPaymentTypeAsync(PaymentType paymentType)
    {
        var movements = await movementRepository.GetAllMovementsByPaymentTypeAsync(paymentType);
        return mapper.Map<List<GetAllMovementsDto>>(movements);
    }

    // Deletar uma movimentação por ID
    public async Task<Movement?> DeleteMovementByIdAsync(Guid id)
    {
        var movement = await movementRepository.GetMovementByIdAsync(id);
        if (movement == null)
        {
            throw new InvalidOperationException("Movimentação não encontrada.");
        }
        
        return await movementRepository.DeleteMovementByIdAsync(id);
    }
    
    // Filtra as movimentações e retorna uma lista
    public async Task<List<ExportMovementDto>> GetMovementsByFilterAsync(string filterType, int? month = null, int? year = null)
    {
        switch (filterType)
        {
            case "last30days":
                var movements30days = await movementRepository.GetMovementsLast30DaysAsync();
                return mapper.Map<List<ExportMovementDto>>(movements30days);

            case "byMonthYear":
                if (month.HasValue && year.HasValue)
                {
                    var movementsMonthYear = await movementRepository.GetMovementsByMonthYearAsync(month.Value, year.Value);
                    return mapper.Map<List<ExportMovementDto>>(movementsMonthYear);
                }
                throw new ArgumentException("Ano ou mês faltando no filtro.");

            case "all":
                var movementsAll = await movementRepository.GetAllMovementsAsync();
                return mapper.Map<List<ExportMovementDto>>(movementsAll);
            default:
                throw new ArgumentException("Filtro inválido");
        }
    }
}