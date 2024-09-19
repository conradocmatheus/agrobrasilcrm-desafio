using back_end.DTOs;

namespace back_end.Services;

public interface IProductService
{
    Task<CreateProductDto> CreateProductAsync(CreateProductDto createProductDto);
    Task<List<ProductDto>> GetAllProducts();
    Task<ProductDto?> GetProductByIdAsync(Guid id);
    Task<ProductDto?> DeleteProductByIdAsync(Guid id);
}