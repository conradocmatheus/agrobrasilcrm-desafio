using back_end.DTOs.MovementDTOs;
using back_end.Services.MovementServices;
using Microsoft.AspNetCore.Mvc;

namespace back_end.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MovementController(IMovementService movementService) : ControllerBase
{
    [HttpPost]
    [Route("post")]
    public async Task<IActionResult> CreateMovement([FromBody] CreateMovementDto createMovementDto)
    {
        try
        {
            // Chama o serviço para criar a movimentação
            var movementDto = await movementService.CreateMovementAsync(createMovementDto);
            // Retorna o status 201 com o resultado
            return CreatedAtAction(nameof(CreateMovement), new { id = movementDto.Id }, movementDto);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro ao criar movimentação: {ex.Message}");
        }
    }
}