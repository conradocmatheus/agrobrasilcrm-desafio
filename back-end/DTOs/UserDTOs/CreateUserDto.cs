using System.ComponentModel.DataAnnotations;
using back_end.CustomActionFilters;

namespace back_end.DTOs.UserDTOs;

public class CreateUserDto
{
    [Required(ErrorMessage = "O nome é obrigatório.")]
    [NoNumbers]
    public string? Name { get; set; }

    [Required(ErrorMessage = "O email é obrigatório.")]
    [EmailAddress(ErrorMessage = "Endereço de email inválido.")]
    public string? Email { get; set; }

    [Required(ErrorMessage = "A data de nascimento é obrigatória.")]
    [DataType(DataType.Date)]
    public DateTimeOffset Birthday { get; set; }
}