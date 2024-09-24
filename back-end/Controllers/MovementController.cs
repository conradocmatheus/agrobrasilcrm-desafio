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
            var movementDto = await movementService.CreateMovementAsync(createMovementDto);
            return Ok(movementDto);
        }
        catch (ArgumentException e)
        {
            return BadRequest(e.Message);
        }
        catch (InvalidOperationException e)
        {
            return BadRequest(e.Message);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
}