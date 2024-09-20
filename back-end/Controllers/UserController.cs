using back_end.DTOs;
using back_end.Services;
using Microsoft.AspNetCore.Mvc;

namespace back_end.Controllers;

// Mudar ordem para melhor organizacao dps
[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    // Injeção de dependência userService
    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    // POST User
    // POST: /api/user
    [HttpPost]
    [Route("/post")]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserDto createUserDto)
    {
        // Primeiramente verifica se o modelo enviado na requisição json é valido
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        // Depois chama o método do UserService e retorna 200 se der certo
        try
        {
            var userDto = await _userService.CreateUserAsync(createUserDto);
            return Ok(userDto);
        }
        // Se não, lança uma exceção genérica
        catch(Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    // GET Users
    // GET: /api/user/by-creation-date
    [HttpGet]
    [Route("/get/by-creation-date")]
    public async Task<IActionResult> GetUsersByCreatedAt()
    {
        try
        {
            // Atribui o usuário criado pra userCreatedDto
            var userCreatedAtDto = await _userService.GetUsersByCreatedAtAsync();
            return Ok(userCreatedAtDto);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    // GET User by id
    // GET: /api/user/by-id
    [HttpGet]
    [Route("/get/by-id/{id:Guid}")]
    public async Task<IActionResult> GetUserById([FromRoute] Guid id)
    {
        try
        {
            // Atribui o usuário encontrado pra selectedUser
            var selectedUser = await _userService.GetUserByIdAsync(id);
            return Ok(selectedUser);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    // Delete User by id
    // Delete: /api/user/by-id
    [HttpDelete]
    [Route("/delete/by-id/{id:Guid}")]
    public async Task<IActionResult> DeleteUserById([FromRoute] Guid id)
    {
        try
        {
            // Atribui o usuário deletado pra deletedUser
            var deletedUser = await _userService.DeleteUserByIdAsync(id);
            return Ok(deletedUser);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    // Update User by id
    // Update: /api/user/by-id
    [HttpPut]
    [Route("/update/by-id/{id:Guid}")]
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
            var userDto = await _userService.UpdateUserAsync(createUserDto, id);
            return Ok(userDto);
        }
        // Se não conseguir, lança uma exception
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

}