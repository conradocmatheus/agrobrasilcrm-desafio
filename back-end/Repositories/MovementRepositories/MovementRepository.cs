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
    // Cria uma nova movimentação
    public async Task<Movement> CreateMovementAsync(Movement movement)
    {
        movement.Id = Guid.NewGuid();
        movement.CreatedAt = DateTime.UtcNow;
        movement.UpdatedAt = DateTime.UtcNow;

        await context.Movements.AddAsync(movement);
        await context.SaveChangesAsync();
        return movement;
    }

    // Retorna movimentações paginadas, incluindo usuário e produtos
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

    // Retorna movimentações por tipo de pagamento
    public async Task<List<Movement>> GetAllMovementsByPaymentTypeAsync(PaymentType paymentType)
    {
        return await context.Movements
            .Include(m => m.MovementProducts)
            .Where(movement => movement.PaymentType == paymentType)
            .ToListAsync();
    }

    // Deleta uma movimentação por ID
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

    // Retorna uma movimentação por ID
    public async Task<Movement?> GetMovementByIdAsync(Guid id)
    {
        return await context.Movements
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    // Retorna o preço de um produto por ID
    public async Task<double> GetProductPriceAsync(Guid productId)
    {
        var product = await context.Products.FindAsync(productId);
        return product != null ? product.Price : 0;
    }


    // Verifica se o usuário existe
    public async Task<bool> UserExistsAsync(Guid userId)
    {
        return await context.Users.AnyAsync(u => u.Id == userId);
    }

    // Verifica se o produto existe
    public async Task<bool> ProductExistsAsync(Guid productId)
    {
        return await context.Products.AnyAsync(p => p.Id == productId);
    }

    // Retorna em lista todas as movimentações dos últimos 30 dias
    public async Task<List<Movement>> GetMovementsLast30DaysAsync()
    {
        var thirtyDaysAgo = DateTime.UtcNow.AddDays(-30);

        return await context.Movements.Include(m => m.User)
            .Include(m => m.User)
            .Include(m => m.MovementProducts)
            .ThenInclude(mp => mp.Product)
            .Where(movement => movement.CreatedAt >= thirtyDaysAgo)
            .OrderBy(movement => movement.CreatedAt)
            .AsNoTracking()
            .ToListAsync();
    }

    // Retorna movimentações de um mês e ano específicos
    public async Task<List<Movement>> GetMovementsByMonthYearAsync(int month, int year)
    {
        var startDate = new DateTime(year, month, 1);
        var endDate = startDate.AddMonths(1).AddDays(-1);

        return await context.Movements.Include(m => m.User)
            .Include(m => m.User)
            .Include(m => m.MovementProducts)
            .ThenInclude(mp => mp.Product)
            .Where(movement => movement.CreatedAt >= startDate && movement.CreatedAt <= endDate)
            .OrderBy(movement => movement.CreatedAt)
            .AsNoTracking()
            .ToListAsync();
    }

    // Retorna todas as movimentações
    public async Task<List<Movement>> GetAllMovementsAsync()
    {
        return await context.Movements.Include(m => m.User)
            .Include(m => m.User)
            .Include(m => m.MovementProducts)
            .ThenInclude(mp => mp.Product)
            .AsNoTracking()
            .ToListAsync();
    }
    

    // Soma de todas as movimentações do tipo débito e retorna um double
    public async Task<double> GetTotalValueDebitAsync()
    {
        return await context.Movements
            .Where(m => m.PaymentType == PaymentType.Debit)
            .SumAsync(m => m.TotalValue);
    }

    // Soma de todas as movimentações do tipo crédito e retorna um double
    public async Task<double> GetTotalValueCreditAsync()
    {
        return await context.Movements
            .Where(m => m.PaymentType == PaymentType.Credit)
            .SumAsync(m => m.TotalValue);
    }
    
    // Soma de todas as movimentações (crédito e débito) e retorna um double
    public async Task<double> GetTotalValueMovementsAsync()
    {
        return await context.Movements
            .SumAsync(m => m.TotalValue);
    }
}