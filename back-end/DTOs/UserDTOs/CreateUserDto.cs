using System.ComponentModel.DataAnnotations;
using back_end.Filters;

namespace back_end.DTOs.UserDTOs;

public class CreateUserDto
{
    public required string Name { get; set; }
    public required string Email { get; set; }
    public required DateTime Birthday { get; set; }
}