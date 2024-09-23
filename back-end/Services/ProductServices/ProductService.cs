using AutoMapper;
using back_end.DTOs;
using back_end.DTOs.UserDTOs;
using back_end.Models;
using back_end.Repositories;
using back_end.Repositories.ProductRepositories;

namespace back_end.Services.ProductServices;

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
        var productDto = _mapper.Map<CreateProductDto>(product);
        return productDto;
    }

    public async Task<ProductDto?> UpdateProductAsync(UpdateProductDto updateProductDto, Guid id)
    {
        var product = _mapper.Map<Product>(updateProductDto);
        await _productRepository.UpdateProductAsync(product, id);
        return _mapper.Map<ProductDto>(updateProductDto);
        // Produto retorna com id toda zerada na respota, mas fica certo no banco
    }
    
    public async Task<ProductDto?> DeleteProductByIdAsync(Guid id)
    {
        var deletedProduct = await _productRepository.DeleteProductByIdAsync(id);
        return deletedProduct == null ? null : _mapper.Map<ProductDto>(deletedProduct);
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
}