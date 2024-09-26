using back_end.CustomActionFilters;
using back_end.DTOs.MovementDTOs;
using back_end.Helpers;
using back_end.Models.Enums;
using back_end.Services.MovementServices;
using Microsoft.AspNetCore.Mvc;

namespace back_end.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MovementController(IMovementService movementService) : ControllerBase
{
    // POST - Movement
    // POST - /api/movement/post
    [HttpPost]
    [Route("post")]
    [ValidadeModel]
    public async Task<IActionResult> CreateMovement([FromBody] CreateMovementDto createMovementDto)
    {
        // Chama o serviço para criar a movimentação
        var movementDto = await movementService.CreateMovementAsync(createMovementDto);
        // Retorna o status 201 com o resultado
        return CreatedAtAction(nameof(CreateMovement), new { id = movementDto.Id }, movementDto);
    }

    // GET - Movements
    // GET - /api/movement/get-all
    [HttpGet]
    [Route("get-all")]
    public async Task<IActionResult> GetAllMovements([FromQuery] QueryObject query)
    {
        var movements = await movementService.GetAllMovementsPaginatedAsync(query);
        if (movements.Count == 0)
        {
            return NotFound("Nenhum movimento encontrado.");
        }

        return Ok(movements);
    }

    // GET - Movements
    // GET - /api/movement/get-all/by-payment-type/{paymentType}
    [HttpGet]
    [Route("get-all/by-payment-type/{paymentType}")]
    public async Task<IActionResult> GetAllMovementsByPaymentType([FromRoute] PaymentType paymentType)
    {
        var movements = await movementService.GetAllMovementsByPaymentTypeAsync(paymentType);
        if (movements.Count == 0)
        {
            return NotFound("Nenhum movimento encontrado.");
        }

        return Ok(movements);
    }
    
    // DELETE - Movement
    // DELETE - /api/movement/delete/{id}
    [HttpDelete]
    [Route("delete/{id:Guid}")]
    public async Task<IActionResult> DeleteMovementById([FromRoute] Guid id)
    {
        var deletedMovement = await movementService.DeleteMovementByIdAsync(id);
        return Ok(deletedMovement);
    }
}