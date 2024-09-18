using System.ComponentModel.DataAnnotations;
using back_end.Filters;

namespace back_end.DTOs;

public class CreateUserDto
{
    [MinLength(1, ErrorMessage = "Name cannot be empty")]// Para o nome nao ser vazio
    [MaxLength(200, ErrorMessage = "Name max character lenght exceeded")]
    public required string Name { get; set; }

    [EmailAddress(ErrorMessage = "Invalid Email")]// Validacao de formato de email e msg de erro
    [MaxLength(200, ErrorMessage = "Email max character lenght exceeded")]
    public required string Email { get; set; }

    [CustomDateRange(MinYear = 1900, MaxYear = 2100, ErrorMessage = "Please stay inside year limits min(1900) max(2100)")]
    public required DateTime Birthday { get; set; }
}