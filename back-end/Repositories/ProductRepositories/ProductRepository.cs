using back_end.Data;
using back_end.Models;
using Microsoft.EntityFrameworkCore;

namespace back_end.Repositories.ProductRepositories;

public class ProductRepository(AppDbContext context) : IProductRepository
{
    // Cria um produto no banco
    public async Task<Product> CreateProductAsync(Product product)
    {
        product.Id = Guid.NewGuid();
        
        context.Products.Add(product);
        await context.SaveChangesAsync();
        return product;
    }

    // Deleta um produto por ID
    public async Task<Product?> DeleteProductByIdAsync(Guid id)
    {
        var existingProduct = await context.Products.FirstOrDefaultAsync(x => x.Id == id);

        if (existingProduct == null)
        {
            return null;
        }
        
        context.Products.Remove(existingProduct);
        await context.SaveChangesAsync();
        return existingProduct;
    }

    // Lista todos os produtos
    public async Task<List<Product>> GetAllProductsAsync()
    {
        return await context.Products.AsNoTracking().ToListAsync();
    }

    // Busca um produto por ID
    public async Task<Product?> GetProductByIdAsync(Guid id)
    {
        return await context.Products.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<int> GetProductQuantityByIdAsync(Guid id)
    {
        var product = await context.Products.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        return product.Quantity;
    }
}