using back_end.CustomActionFilters;
using back_end.DTOs;
using back_end.DTOs.ProductDTOs;
using back_end.DTOs.UserDTOs;
using back_end.Services.ProductServices;
using Microsoft.AspNetCore.Mvc;

namespace back_end.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController(IProductService productService) : ControllerBase
{
    // POST - Product
    // POST - /api/product/post
    [HttpPost]
    [ValidadeModel]
    [Route("post")]
    public async Task<IActionResult> CreateProduct([FromBody] CreateProductDto createProductDto)
    {
        // Chama o método do productService para criar o product
        var productDto = await productService.CreateProductAsync(createProductDto);

        // Retorna 201 se der certo
        return CreatedAtAction(nameof(GetProductById), new { id = productDto.Id }, productDto);
    }

    // PUT - Product by id
    // PUT - /api/product/update/{id}
    [HttpPut]
    [ValidadeModel]
    [Route("update/{id:Guid}")]
    public async Task<IActionResult> UpdateProductById([FromBody] UpdateProductDto updateProductDto,
        [FromRoute] Guid id)
    {
        var productDto = await productService.UpdateProductAsync(updateProductDto, id);

        if (productDto == null)
        {
            return NotFound($"Produto com  ID {id} nao encontrado"); // Retorna notFound se não encontrado
        }

        return Ok(productDto);
    }

    // DELETE - Product by id
    // DELETE - /api/product/delete/{id}
    [HttpDelete]
    [Route("delete/{id:Guid}")]
    public async Task<IActionResult> DeleteProductById([FromRoute] Guid id)
    {
        // Atribui o produto deletado pra deletedProduct
        var deletedProduct = await productService.DeleteProductByIdAsync(id);

        if (deletedProduct == null)
        {
            return NotFound($"Produto com ID {id} nao encontrado."); // Retorna notFound se não encontrado
        }

        return Ok(deletedProduct);
    }

    // GET - Products
    // GET - /api/product/get-all
    [HttpGet]
    [Route("get-all")]
    public async Task<IActionResult> GetAllProducts()
    {
        // Atribui a lista de produtos pra productDto
        var productDto = await productService.GetAllProductsAsync();
        // Verifica se tem produto na lista
        if (productDto.Count == 0)
        {
            return NotFound("Nenhum produto encontrado");
        }

        return Ok(productDto);
    }

    // GET - Products by id
    // GET - /api/product/{id}
    [HttpGet]
    [Route("{id:Guid}")]
    public async Task<IActionResult> GetProductById(Guid id)
    {
        // Atribui o produto encontrado pra selectedProduct
        var selectedProduct = await productService.GetProductByIdAsync(id);
        // Verifica se o produto foi encontrado
        if (selectedProduct == null)
        {
            return NotFound($"Produto com ID {id} nao foi encontrado");
        }

        return Ok(selectedProduct);
    }
}