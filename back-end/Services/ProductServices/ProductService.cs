using AutoMapper;
using back_end.DTOs;
using back_end.DTOs.ProductDTOs;
using back_end.DTOs.UserDTOs;
using back_end.Models;
using back_end.Repositories;
using back_end.Repositories.ProductRepositories;

namespace back_end.Services.ProductServices;

public class ProductService(IMapper mapper, IProductRepository productRepository) : IProductService
{
    // Criar Produtos
    public async Task<ProductDto> CreateProductAsync(CreateProductDto createProductDto)
    {
        var product = mapper.Map<Product>(createProductDto);
        await productRepository.CreateProductAsync(product);
        return mapper.Map<ProductDto>(product);
    }

    // Atualizar Produtos por ID
    public async Task<ProductDto?> UpdateProductAsync(UpdateProductDto updateProductDto, Guid id)
    {
        var existingProduct = await productRepository.GetProductByIdAsync(id);

        if (existingProduct == null)
        {
            return null;
        }
        mapper.Map(updateProductDto, existingProduct);
        
        await productRepository.UpdateProductAsync(existingProduct, id);
        var updatedProduct = await productRepository.GetProductByIdAsync(id);
        return mapper.Map<ProductDto>(updatedProduct);
    }

    // Deletar Produtos por ID
    public async Task<ProductDto?> DeleteProductByIdAsync(Guid id)
    {
        var deletedProduct = await productRepository.DeleteProductByIdAsync(id);
        return deletedProduct == null ? null : mapper.Map<ProductDto>(deletedProduct);
    }

    // Listar Produtos
    public async Task<List<ProductDto>> GetAllProductsAsync()
    {
        var products = await productRepository.GetAllProductsAsync();
        return mapper.Map<List<ProductDto>>(products);
    }

    // Encontrar Produto por ID
    public async Task<ProductDto?> GetProductByIdAsync(Guid id)
    {
        var foundProduct = await productRepository.GetProductByIdAsync(id);
        return foundProduct == null ? null : mapper.Map<ProductDto>(foundProduct);
    }
}