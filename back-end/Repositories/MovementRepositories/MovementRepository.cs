using System.Globalization;
using back_end.Data;
using back_end.Helpers;
using back_end.Models;
using back_end.Models.Enums;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace back_end.Repositories.MovementRepositories;

public class MovementRepository(AppDbContext context) : IMovementRepository
{
    // Criar uma movimentação
    public async Task<Movement> CreateMovementAsync(Movement movement)
    {
        // Cria um id do tipo GUID pra movimentação
        movement.Id = Guid.NewGuid();
        // Atribui o valor do UTC now para as datas
        movement.CreatedAt = DateTime.UtcNow;
        movement.UpdatedAt = DateTime.UtcNow;

        // Adiciona a movimentação no banco
        await context.Movements.AddAsync(movement);
        // Salva as alterações
        await context.SaveChangesAsync();
        // Retorna a movimentação criada
        return movement;
    }

    // Listar movimentações paginadas e mostrando info do user
    public async Task<List<Movement>> GetAllMovementsPaginatedAsync(QueryObject query)
    {
        var skipNumber = (query.PageNumber - 1) * query.PageSize;

        return await context.Movements
            .Include(m => m.MovementProducts)
            .Include(m => m.User)
            .AsNoTracking()
            .Skip(skipNumber)
            .Take(query.PageSize)
            .ToListAsync();
    }

    // Listar todas no débito ou no crédito
    public async Task<List<Movement>> GetAllMovementsByPaymentTypeAsync(PaymentType paymentType)
    {
        return await context.Movements
            .Include(m => m.MovementProducts)
            .Where(movement => movement.PaymentType == paymentType)
            .ToListAsync();
    }

    // Deletar uma movimentação por id
    public async Task<Movement?> DeleteMovementByIdAsync(Guid id)
    {
        var existingMovement = await context.Movements.FirstOrDefaultAsync(x => x.Id == id);

        if (existingMovement == null)
        {
            return null;
        }

        context.Movements.Remove(existingMovement);
        await context.SaveChangesAsync();
        return existingMovement;
    }

    // Get movement by id
    public async Task<Movement?> GetMovementByIdAsync(Guid id)
    {
        return await context.Movements
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);
    }
    
    // Pegar o preço do produto
    public async Task<double> GetProductPriceAsync(Guid productId)
    {
        var product = await context.Products.FindAsync(productId);
        return product != null ? product.Price : 0;
    }
    

    // Checar se o usuário existe
    public async Task<bool> UserExistsAsync(Guid userId)
    {
        return await context.Users.AnyAsync(u => u.Id == userId);
    }

    // Checar se o produto existe
    public async Task<bool> ProductExistsAsync(Guid productId)
    {
        return await context.Products.AnyAsync(p => p.Id == productId);
    }
    
    // Listar todas as movimentações nos últimos 30 dias e retorna uma lista
    public async Task<List<Movement>> GetMovementsLast30DaysAsync()
    {
        var thirtyDaysAgo = DateTime.UtcNow.AddDays(-30);

        return await context.Movements
            .Where(movement => movement.CreatedAt >= thirtyDaysAgo)
            .OrderBy(movement => movement.CreatedAt)
            .AsNoTracking()
            .ToListAsync();
    }
    
    // Listar movimentações de um mês e ano específicos e retorna em lista
    public async Task<List<Movement>> GetMovementsByMonthYearAsync(int month, int year)
    {
        // Cria as datas de início e fim do mês
        var startDate = new DateTime(year, month, 1);
        var endDate = startDate.AddMonths(1).AddDays(-1);

        return await context.Movements
            .Where(movement => movement.CreatedAt >= startDate && movement.CreatedAt <= endDate)
            .OrderBy(movement => movement.CreatedAt)
            .AsNoTracking()
            .ToListAsync();
    }
    
    // Pega todas as movimentações e retorna uma lista
    public async Task<List<Movement>> GetAllMovementsAsync()
    {
        return await context.Movements.ToListAsync();
    }
}