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
        // Mapeia o createProductDto pra product
        var product = mapper.Map<Product>(createProductDto);

        // Chama o método do repositório que salva o produto no banco
        await productRepository.CreateProductAsync(product);

        // Retorna o CreateProductDto mapeado de product
        return mapper.Map<ProductDto>(product);
    }

    // Atualizar Produtos por ID
    public async Task<ProductDto?> UpdateProductAsync(UpdateProductDto updateProductDto, Guid id)
    {
        
        // Busca o produto antes do update por id do banco
        var existingProduct = await productRepository.GetProductByIdAsync(id);

        if (existingProduct == null)
        {
            return null;
        }
        mapper.Map(updateProductDto, existingProduct);
        
        // Chama o update do repositório
        await productRepository.UpdateProductAsync(existingProduct, id);

        // Busca o produto atualizado
        var updatedProduct = await productRepository.GetProductByIdAsync(id);

        // Retorna o produto atualizado mapeado para ProductDto
        return mapper.Map<ProductDto>(updatedProduct);
    }

    // Deletar Produtos por ID
    public async Task<ProductDto?> DeleteProductByIdAsync(Guid id)
    {
        // Atribui o produto deletado a uma variável(deletedProduct)
        var deletedProduct = await productRepository.DeleteProductByIdAsync(id);
        // Retorna o produto ou null
        return deletedProduct == null ? null : mapper.Map<ProductDto>(deletedProduct);
    }

    // Listar Produtos
    public async Task<List<ProductDto>> GetAllProductsAsync()
    {
        // Atribui a lista dos produtos para a variável products
        var products = await productRepository.GetAllProductsAsync();
        // Retorna mapeado para Dto
        return mapper.Map<List<ProductDto>>(products);
    }

    // Encontrar Produto por ID
    public async Task<ProductDto?> GetProductByIdAsync(Guid id)
    {
        // Procura o produto e atribui ele para a variável(foundProduct) se for encontrado
        var foundProduct = await productRepository.GetProductByIdAsync(id);
        // Retorna o produto encontrado ou null, mapeado pra productDto
        return foundProduct == null ? null : mapper.Map<ProductDto>(foundProduct);
    }
}