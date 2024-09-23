using System.ComponentModel.DataAnnotations;
using back_end.Filters;

namespace back_end.DTOs.UserDTOs;

public class CreateUserDto
{
    [MinLength(1, ErrorMessage = "Nome não pode ser vazio")]// Para o nome não ser vazio
    [MaxLength(200, ErrorMessage = "Não ultrapasse 200 caracteres")]
    public required string Name { get; set; }

    [EmailAddress(ErrorMessage = "Email invalido")]// Validação de formato de email e mensagem de erro
    [MaxLength(200, ErrorMessage = "Limite de 200 caracteres excedido")]
    public required string Email { get; set; }

    // Anotação personalizada para ano mínimo e máximo
    [CustomDateRange(MinYear = 1900, MaxYear = 2100, ErrorMessage = "Ano mínimo e máximo permitidos (1900) e (2100)")]
    public required DateTime Birthday { get; set; }
}