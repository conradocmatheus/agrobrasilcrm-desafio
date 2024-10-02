using back_end.CustomActionFilters;
using back_end.DTOs.ProductDTOs;
using back_end.Services.ProductServices;
using Microsoft.AspNetCore.Mvc;

namespace back_end.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController(IProductService productService) : ControllerBase
{
    /// <summary>
    /// Cria um novo produto.
    /// </summary>
    /// <param name="createProductDto">Dados necessários para criação do produto.</param>
    /// <returns>Um objeto de produto foi criado.</returns>
    /// <response code="201">Produto criado com sucesso.</response>
    /// <response code="400">Dados enviados inválidos.</response>
    /// <response code="500">Erro interno inesperado.</response>
    [HttpPost]
    [ValidadeModel]
    [Route("")]
    public async Task<IActionResult> CreateProduct([FromBody] CreateProductDto createProductDto)
    {
        var productDto = await productService.CreateProductAsync(createProductDto);
        
        return CreatedAtAction(nameof(GetProductById), new { id = productDto.Id }, productDto);
    }

    /// <summary>
    /// Atualiza um produto por ID.
    /// </summary>
    /// <param name="updateProductDto">Dados necessários para atualização do produto.</param>
    /// <param name="id">ID do produto a ser atualizado.</param>
    /// <returns>O produto atualizado.</returns>
    /// <response code="200">Produto atualizado com sucesso.</response>
    /// <response code="404">Produto não encontrado.</response>
    /// <response code="400">Dados enviados inválidos.</response>
    /// <response code="500">Erro interno inesperado.</response>
    [HttpPut]
    [ValidadeModel]
    [Route("{id:Guid}")]
    public async Task<IActionResult> UpdateProductById([FromBody] UpdateProductDto updateProductDto,
        [FromRoute] Guid id)
    {
        var productDto = await productService.UpdateProductAsync(updateProductDto, id);

        if (productDto == null)
        {
            return NotFound($"Produto com  ID {id} nao encontrado");
        }

        return Ok(productDto);
    }

    /// <summary>
    /// Apaga um produto por ID.
    /// </summary>
    /// <param name="id">ID do produto a ser apagado.</param>
    /// <returns>O produto apagado.</returns>
    /// <response code="200">Produto apagado com sucesso.</response>
    /// <response code="404">Produto não encontrado.</response>
    /// <response code="500">Erro interno inesperado.</response>
    [HttpDelete]
    [Route("{id:Guid}")]
    public async Task<IActionResult> DeleteProductById([FromRoute] Guid id)
    {
        var deletedProduct = await productService.DeleteProductByIdAsync(id);

        if (deletedProduct == null)
        {
            return NotFound($"Produto com ID {id} nao encontrado.");
        }

        return Ok(deletedProduct);
    }

    /// <summary>
    /// Retorna todos os produtos.
    /// </summary>
    /// <returns>Lista de produtos.</returns>
    /// <response code="200">Lista de produtos retornada com sucesso.</response>
    /// <response code="404">Nenhum produto encontrado.</response>
    /// <response code="500">Erro interno inesperado.</response>
    [HttpGet]
    [Route("")]
    public async Task<IActionResult> GetAllProducts()
    {
        var productDto = await productService.GetAllProductsAsync();
        
        if (productDto.Count == 0)
        {
            return NotFound("Nenhum produto encontrado");
        }

        return Ok(productDto);
    }

    /// <summary>
    /// Retorna um produto por ID.
    /// </summary>
    /// <param name="id">ID do produto a ser buscado.</param>
    /// <returns>O produto correspondente ao ID fornecido.</returns>
    /// <response code="200">Produto encontrado e retornado com sucesso.</response>
    /// <response code="404">Produto não encontrado.</response>
    /// <response code="500">Erro interno inesperado.</response> 
    [HttpGet]
    [Route("{id:Guid}")]
    public async Task<IActionResult> GetProductById(Guid id)
    {
        var selectedProduct = await productService.GetProductByIdAsync(id);
        
        if (selectedProduct == null)
        {
            return NotFound($"Produto com ID {id} nao foi encontrado");
        }

        return Ok(selectedProduct);
    }
}