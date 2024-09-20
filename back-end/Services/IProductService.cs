using back_end.DTOs;

namespace back_end.Services;

public interface IProductService
{
    Task<CreateProductDto> CreateProductAsync(CreateProductDto createProductDto);
    Task<ProductDto?> DeleteProductByIdAsync(Guid id);
    Task<ProductDto?> UpdateProductAsync(UpdateProductDto updateProductDto, Guid id);
    Task<List<ProductDto>> GetAllProducts();
    Task<ProductDto?> GetProductByIdAsync(Guid id);
    
}