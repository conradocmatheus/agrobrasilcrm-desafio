using back_end.DTOs;
using back_end.Services;
using Microsoft.AspNetCore.Mvc;

namespace back_end.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    // POST Product
    // POST: /api/product/post
    [HttpPost]
    [Route("post")]
    public async Task<IActionResult> CreateProduct([FromBody] CreateProductDto createProductDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var productDto = await _productService.CreateProductAsync(createProductDto);
            return Ok(productDto);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    // PUT Product by id
    // PUT: /api/product/update/{id}
    [HttpPut]
    [Route("update/{id:Guid}")]
    public async Task<IActionResult> UpdateProductById([FromBody] UpdateProductDto updateProductDto,
        [FromRoute] Guid id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var productDto = await _productService.UpdateProductAsync(updateProductDto, id);
            return Ok(productDto);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    // DELETE Product by id
    // DELETE: /api/product/delete/{id}
    [HttpDelete]
    [Route("delete/{id:Guid}")]
    public async Task<IActionResult> DeleteProductById([FromRoute] Guid id)
    {
        try
        {
            var deletedUser = await _productService.DeleteProductByIdAsync(id);
            return Ok(deletedUser);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    // GET Products
    // GET: /api/product/get-all
    [HttpGet]
    [Route("get-all")]
    public async Task<IActionResult> GetAllProducts()
    {
        try
        {
            var productDto = await _productService.GetAllProducts();
            return Ok(productDto);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}