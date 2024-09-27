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
        var productDto = await productService.CreateProductAsync(createProductDto);
        
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
        var productDto = await productService.GetAllProductsAsync();
        
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
        var selectedProduct = await productService.GetProductByIdAsync(id);
        
        if (selectedProduct == null)
        {
            return NotFound($"Produto com ID {id} nao foi encontrado");
        }

        return Ok(selectedProduct);
    }
}