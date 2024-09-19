using back_end.Models;

namespace back_end.Repositories;

public interface IProductRepository
{
    Task AddProductAsync(Product product);
    Task<List<Product>> GetAllProductsAsync();
    Task<Product?> GetProductByIdAsync(Guid id);
    Task<Product?> DeleteProductByIdAsync(Guid id);
}