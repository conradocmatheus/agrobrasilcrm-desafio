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
        try
        {
            // Mapeia o createProductDto pra product
            var product = mapper.Map<Product>(createProductDto);
            
            // Chama o método do repositório que salva o usuário no banco
            await productRepository.CreateProductAsync(product);
            
            // Retorna o CreateProductDto mapeado de product
            return mapper.Map<ProductDto>(product);
        }
        catch (Exception e)
        {
            throw new InvalidOperationException("Erro ao criar produto.", e);
        }
    }

    // Atualizar Produtos por ID
    public async Task<ProductDto?> UpdateProductAsync(UpdateProductDto updateProductDto, Guid id)
    {
        try
        {
            // Faz o mapeamento de updateProductDto pra Product
            var toUpdateProduct = mapper.Map<Product>(updateProductDto);
            
            // Chama o update do repositório
            await productRepository.UpdateProductAsync(toUpdateProduct, id);
            
            // E Retorna o produto mapeado
            return mapper.Map<ProductDto>(updateProductDto);
            //============= CORRIGIR
            // Produto retorna com id toda zerada na resposta, mas fica certo no banco
            //============= CORRIGIR
        }   
        catch (Exception e)
        {
            throw new InvalidOperationException("Erro ao atualizar produto.", e);
        }
    }
    
    // Deletar Produtos por ID
    public async Task<ProductDto?> DeleteProductByIdAsync(Guid id)
    {
        try
        {
            // Atribui o produto deletado a uma variável(deletedProduct)
            var deletedProduct = await productRepository.DeleteProductByIdAsync(id);
            // Retorna o produto ou null
            return deletedProduct == null ? null : mapper.Map<ProductDto>(deletedProduct);
        }
        catch (Exception e)
        {
            throw new InvalidOperationException("Erro ao deletar produto.", e);
        }
    }

    // Listar Produtos // Mudar nome para async dps
    public async Task<List<ProductDto>> GetAllProducts()
    {
        try
        {
            // Atribui a lista dos produtos para a variável products
            var products = await productRepository.GetAllProductsAsync();
            // Retorna mapeado para Dto
            return mapper.Map<List<ProductDto>>(products);
        }
        catch (Exception e)
        {
            throw new InvalidOperationException("Erro ao obter lista de produtos.", e);
        }
        
    }

    // Encontrar Produto por ID
    public async Task<ProductDto?> GetProductByIdAsync(Guid id)
    {
        try
        {
            // Procura o produto e atribui ele para a variável(foundProduct) se for encontrado
            var foundProduct = await productRepository.GetProductByIdAsync(id);
            // Retorna o produto encontrado ou null, mapeado pra productDto
            return foundProduct == null ? null : mapper.Map<ProductDto>(foundProduct);
        }
        catch (Exception e)
        {
            throw new InvalidOperationException("Erro ao obter produto por ID.", e);
        }
    }
}