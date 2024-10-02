using back_end.DTOs;
using back_end.DTOs.ProductDTOs;
using back_end.DTOs.UserDTOs;

namespace back_end.Services.ProductServices;

public interface IProductService
{
    Task<ProductDto> CreateProductAsync(CreateProductDto createProductDto);
    Task<ProductDto?> DeleteProductByIdAsync(Guid id);
    Task<List<ProductDto>> GetAllProductsAsync();
    Task<ProductDto?> GetProductByIdAsync(Guid id);
}