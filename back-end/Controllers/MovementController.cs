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

    // GET - Movements
    // GET - /api/movement/get-all
    [HttpGet]
    [Route("get-all")]
    public async Task<IActionResult> GetAllMovements([FromQuery] QueryObject query)
    {
        try
        {
            var movements = await movementService.GetAllMovementsAsync(query);
            if (movements.Count == 0)
            {
                return NotFound("Nenhum movimento encontrado.");
            }
            return Ok(movements);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
        
    }

    // GET - Movements
    // GET - /api/movement/get-all/by-payment-type/{paymentType}
    [HttpGet]
    [Route("get-all/by-payment-type/{paymentType}")]
    public async Task<IActionResult> GetAllMovementsByPaymentType([FromRoute] PaymentType paymentType)
    {
        try
        {
            var movements = await movementService.GetAllMovementsByPaymentTypeAsync(paymentType);
            if (movements.Count == 0)
            {
                return NotFound("Nenhum movimento encontrado.");
            }
            return Ok(movements);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
}