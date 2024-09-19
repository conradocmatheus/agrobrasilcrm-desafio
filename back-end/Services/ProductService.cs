using AutoMapper;
using back_end.DTOs;
using back_end.Models;
using back_end.Repositories;

namespace back_end.Services;

public class ProductService : IProductService
{
    private readonly IMapper _mapper;
    private readonly IProductRepository _productRepository;

    public ProductService(IMapper mapper, IProductRepository productRepository)
    {
        _mapper = mapper;
        _productRepository = productRepository;
    }
    
    public async Task<CreateProductDto> CreateProductAsync(CreateProductDto createProductDto)
    {
        var product = _mapper.Map<Product>(createProductDto);
        await _productRepository.CreateProductAsync(product);
        return _mapper.Map<CreateProductDto>(createProductDto);
    }

    public async Task<List<ProductDto>> GetAllProducts()
    {
        var products = await _productRepository.GetAllProductsAsync();
        return _mapper.Map<List<ProductDto>>(products);
    }

    public async Task<ProductDto?> GetProductByIdAsync(Guid id)
    {
        var foundProduct = await _productRepository.GetProductByIdAsync(id);
        return foundProduct == null ? null : _mapper.Map<ProductDto>(foundProduct);
    }

    public async Task<ProductDto?> DeleteProductByIdAsync(Guid id)
    {
        var deletedProduct = await _productRepository.DeleteProductByIdAsync(id);
        return deletedProduct == null ? null : _mapper.Map<ProductDto>(deletedProduct);
    }
}