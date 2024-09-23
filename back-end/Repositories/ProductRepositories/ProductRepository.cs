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
    
    public async Task CreateProductAsync(Product product)
    {
        _context.Products.Add(product);
        await _context.SaveChangesAsync();
    }
    
    public async Task<Product?> UpdateProductAsync(Product product, Guid id)
    {
        var toUpdateProduct = await _context.Products.FirstOrDefaultAsync(x => x.Id == id);

        if (toUpdateProduct == null)
        {
            return null;
        }

        toUpdateProduct.Name = product.Name;
        toUpdateProduct.Price = product.Price;
        toUpdateProduct.Quantity = product.Quantity;

        await _context.SaveChangesAsync();
        return null;
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

    public async Task<List<Product>> GetAllProductsAsync()
    {
        return await _context.Products.ToListAsync();
    }

    public async Task<Product?> GetProductByIdAsync(Guid id)
    {
        return await _context.Products.FirstOrDefaultAsync(x => x.Id == id);
    }
}