using back_end.CustomActionFilters;
using back_end.DTOs;
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
        // Bloco try-catch para tentar chamar o método do productService
        try
        {
            // Chama o método do productService para criar o product
            var productDto = await productService.CreateProductAsync(createProductDto);
            // Retorna 201 se der certo
            return CreatedAtAction(nameof(GetProductById), new { id = productDto.Id }, productDto);
        }
        catch (ArgumentException e) // Para argumentos inválidos
        {
            return BadRequest(e.Message);
        }
        catch (InvalidOperationException e) // Para operações invalidas
        {
            return BadRequest(e.Message);
        }
        catch (Exception e) // Para erros genéricos
        {
            return StatusCode(500, e.Message);
        }
    }

    // PUT - Product by id
    // PUT - /api/product/update/{id}
    [HttpPut]
    [ValidadeModel]
    [Route("update/{id:Guid}")]
    public async Task<IActionResult> UpdateProductById([FromBody] UpdateProductDto updateProductDto,
        [FromRoute] Guid id)
    {
        // Tenta atualizar o produto chamando o método do service
        try
        {
            var productDto = await productService.UpdateProductAsync(updateProductDto, id);

            if (productDto == null)
            {
                return NotFound($"Produto com  ID {id} nao encontrado"); // Retorna notFound se não encontrado
            }
            
            return Ok(productDto);
        }
        // Se não conseguir, lança uma exception
        catch (Exception e)
        {
            return BadRequest("Erro ao atualizar produto: " + e.Message);
        }
    }

    // DELETE - Product by id
    // DELETE - /api/product/delete/{id}
    [HttpDelete]
    [Route("delete/{id:Guid}")]
    public async Task<IActionResult> DeleteProductById([FromRoute] Guid id)
    {
        try
        {
            // Atribui o produto deletado pra deletedProduct
            var deletedProduct = await productService.DeleteProductByIdAsync(id);

            if (deletedProduct == null)
            {
                return NotFound($"Produto com ID {id} nao encontrado."); // Retorna notFound se não encontrado
            }
            
            return Ok(deletedProduct);
        }
        catch (Exception e)
        {
            return BadRequest("Erro ao deletar produto: " + e.Message);
        }
    }

    // GET - Products
    // GET - /api/product/get-all
    [HttpGet]
    [Route("get-all")]
    public async Task<IActionResult> GetAllProducts()
    {
        try
        {
            // Atribui a lista de produtos pra productDto
            var productDto = await productService.GetAllProducts();
            // Verifica se tem produto na lista
            if (productDto.Count == 0)
            {
                return NotFound("Nenhum produto encontrado");
            }
            return Ok(productDto);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

    // GET - Products by id
    // GET - /api/product/{id}
    [HttpGet]
    [Route("{id:Guid}")]
    public async Task<IActionResult> GetProductById(Guid id)
    {
        try
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
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
}