using AutoMapper;
using back_end.DTOs;
using back_end.Models;
using back_end.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace back_end.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IUserRepository _userRepository;

    public UserController(IMapper mapper, IUserRepository userRepository)
    {
        _mapper = mapper;
        _userRepository = userRepository;
    }

    // POST User
    // POST: /api/user
    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserDto createUserDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var user = _mapper.Map<User>(createUserDto);
        user.CreatedAt = DateTime.Now;
        user.UpdatedAt = DateTime.Now;

        await _userRepository.AddUserAsync(user);
        return Ok(_mapper.Map<UserDto>(user));
    }
}