using back_end.CustomActionFilters;
using back_end.DTOs.UserDTOs;
using back_end.Services.UserServices;
using Microsoft.AspNetCore.Mvc;

namespace back_end.Controllers;

// Mudar ordem para melhor organizacao dps
// Melhorar os catchs, retorno errado
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
        // Chama o método do UserService pra criar o usuário
        var userDto = await userService.CreateUserAsync(createUserDto);

        // Retorna 201 se der certo
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
            return NotFound($"Usuário com ID {id} não encontrado."); // Retorna NotFound se o usuário não existir
        }

        return Ok(userDto);
    }

    // Delete - User by id
    // Delete - /api/user/by-id
    [HttpDelete]
    [Route("delete/by-id/{id:Guid}")]
    public async Task<IActionResult> DeleteUserById([FromRoute] Guid id)
    {
        // Atribui o usuário deletado pra deletedUser
        var deletedUser = await userService.DeleteUserByIdAsync(id);

        if (deletedUser == null)
        {
            return NotFound($"Usuário com ID {id} não encontrado."); // Retorna NotFound se o usuário não existir
        }

        return Ok(deletedUser);
    }

    // GET - Users
    // GET - /api/user/by-creation-date
    [HttpGet]
    [Route("get/by-creation-date")]
    public async Task<IActionResult> GetUsersByCreatedAt()
    {
        // Atribui a lista de usuarios pra userCreatedAtDto
        var userCreatedAtDto = await userService.GetUsersByCreatedAtAsync();
        // Verifica se tem algum usuário na lista
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
        // Atribui o usuário encontrado pra selectedUser
        var selectedUser = await userService.GetUserByIdAsync(id);
        // Verifica se o usuário foi encontrado
        if (selectedUser == null)
        {
            return NotFound($"Usuário com ID {id} não foi encontrado");
        }

        return Ok(selectedUser);
    }
}