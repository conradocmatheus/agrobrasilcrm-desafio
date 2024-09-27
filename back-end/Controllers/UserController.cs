using back_end.CustomActionFilters;
using back_end.DTOs.UserDTOs;
using back_end.Services.UserServices;
using Microsoft.AspNetCore.Mvc;

namespace back_end.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController(IUserService userService) : ControllerBase
{
    // POST - User
    // POST - /api/user
    [HttpPost]
    [ValidadeModel]
    [Route("post")]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserDto createUserDto)
    {
        var userDto = await userService.CreateUserAsync(createUserDto);
        
        return CreatedAtAction(nameof(GetUserById), new { id = userDto.Id }, userDto);
    }
    
    // Update - User by id
    // Update - /api/user/by-id
    [HttpPut]
    [ValidadeModel]
    [Route("update/by-id/{id:Guid}")]
    public async Task<IActionResult> UpdateUserById([FromBody] CreateUserDto createUserDto, [FromRoute] Guid id)
    {
        var userDto = await userService.UpdateUserAsync(createUserDto, id);

        if (userDto == null)
        {
            return NotFound($"Usuário com ID {id} não encontrado.");
        }

        return Ok(userDto);
    }

    // Delete - User by id
    // Delete - /api/user/by-id
    [HttpDelete]
    [Route("delete/by-id/{id:Guid}")]
    public async Task<IActionResult> DeleteUserById([FromRoute] Guid id)
    {
        var deletedUser = await userService.DeleteUserByIdAsync(id);

        if (deletedUser == null)
        {
            return NotFound($"Usuário com ID {id} não encontrado.");
        }

        return Ok(deletedUser);
    }

    // GET - Users
    // GET - /api/user/by-creation-date
    [HttpGet]
    [Route("get/by-creation-date")]
    public async Task<IActionResult> GetUsersByCreatedAt()
    {
        var userCreatedAtDto = await userService.GetUsersByCreatedAtAsync();
        
        if (userCreatedAtDto.Count == 0)
        {
            return NotFound("Nenhum usuário foi encontrado");
        }

        return Ok(userCreatedAtDto);
    }

    // GET - User by id
    // GET - /api/user/by-id
    [HttpGet]
    [Route("get/by-id/{id:Guid}")]
    public async Task<IActionResult> GetUserById([FromRoute] Guid id)
    {
        var selectedUser = await userService.GetUserByIdAsync(id);
        
        if (selectedUser == null)
        {
            return NotFound($"Usuário com ID {id} não foi encontrado");
        }

        return Ok(selectedUser);
    }
}