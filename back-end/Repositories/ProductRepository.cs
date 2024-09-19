using back_end.Data;
using back_end.Models;
using Microsoft.EntityFrameworkCore;

namespace back_end.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly AppDbContext _context;

    public ProductRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task AddProductAsync(Product product)
    {
        _context.Products.Add(product);
        await _context.SaveChangesAsync();
    }

    public async Task<List<Product>> GetAllProductsAsync()
    {
        return await _context.Products.ToListAsync();
    }

    public async Task<Product?> GetProductByIdAsync(Guid id)
    {
        return await _context.Products.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<Product?> DeleteProductByIdAsync(Guid id)
    {
        var existingProduct = await _context.Products.FirstOrDefaultAsync(x => x.Id == id);

        if (existingProduct == null)
        {
            return null;
        }

        _context.Products.Remove(existingProduct);
        await _context.SaveChangesAsync();
        return existingProduct;
    }
}