using AutoMapper;
using back_end.DTOs;
using back_end.Models;
using back_end.Repositories;
using back_end.Services;
using Microsoft.AspNetCore.Mvc;

namespace back_end.Controllers;

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
}