using System.ComponentModel.DataAnnotations;

namespace back_end.DTOs;

public class CreateUserDto
{
    [MaxLength(100)]
    public required string Name { get; set; }

    [EmailAddress]
    public string? Email { get; set; }

    public DateTime Birthday { get; set; }
}