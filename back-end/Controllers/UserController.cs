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
    [Route("post")]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserDto createUserDto)
    {
        // Verifica se o modelo enviado na requisição JSON é valido
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        // Bloco try-catch para tentar chamar o método do userService
        try
        {
            // Chama o método do UserService pra criar o usuário
            var userDto = await userService.CreateUserAsync(createUserDto);
            // Retorna 201 se der certo
            return CreatedAtAction(nameof(GetUserById), new { id = userDto.Id }, userDto);
        }
        catch (ArgumentException e) // Para argumentos inválidos
        {
            return BadRequest(e.Message);
        }
        catch (InvalidOperationException e) // Para operações inválidas
        {
            return BadRequest(e.Message);
        }
        catch (Exception e) // Para erros genéricos
        {
            return StatusCode(500, e.Message);
        }
    }
    
    //========================
    // AINDA ESTA SENDO POSSIVEL ATUALIZAR E COLOCAR IDADE MENOR DE 18
    //========================
    // Update - User by id
    // Update - /api/user/by-id
    [HttpPut]
    [Route("update/by-id/{id:Guid}")]
    public async Task<IActionResult> UpdateUserById([FromBody] CreateUserDto createUserDto, [FromRoute] Guid id)
    {
        // Verifica o corpo da requisição, se esta tudo nos conformes...
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        // Tenta atualizar o usuário chamando o método do service
        try
        {
            var userDto = await userService.UpdateUserAsync(createUserDto, id);

            if (userDto == null)
            {
                return NotFound($"Usuário com ID {id} não encontrado."); // Retorna NotFound se o usuário não existir
            }

            return Ok(userDto);
        }
        // Se não conseguir, lança uma exception
        catch (Exception e)
        {
            return BadRequest("Erro ao atualizar usuário: " + e.Message);
        }
    }
    
    // Delete - User by id
    // Delete - /api/user/by-id
    [HttpDelete]
    [Route("delete/by-id/{id:Guid}")]
    public async Task<IActionResult> DeleteUserById([FromRoute] Guid id)
    {
        try
        {
            // Atribui o usuário deletado pra deletedUser
            var deletedUser = await userService.DeleteUserByIdAsync(id);

            if (deletedUser == null)
            {
                return NotFound($"Usuário com ID {id} não encontrado."); // Retorna NotFound se o usuário não existir
            }

            return Ok(deletedUser);
        }
        catch (Exception e)
        {
            return BadRequest("Erro ao deletar usuário: " + e.Message);
        }
    }
    
    // GET - Users
    // GET - /api/user/by-creation-date
    [HttpGet]
    [Route("get/by-creation-date")]
    public async Task<IActionResult> GetUsersByCreatedAt()
    {
        try
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
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
    
    // GET - User by id
    // GET - /api/user/by-id
    [HttpGet]
    [Route("get/by-id/{id:Guid}")]
    public async Task<IActionResult> GetUserById([FromRoute] Guid id)
    {
        try
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
        catch (Exception e)
        {
            return StatusCode(500, "Erro interno no servidor.");
        }
    }
}