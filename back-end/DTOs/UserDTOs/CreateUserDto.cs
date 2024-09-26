using System.ComponentModel.DataAnnotations;
using back_end.CustomActionFilters;

namespace back_end.DTOs.UserDTOs;

public class CreateUserDto
{
    [Required(ErrorMessage = "O nome é obrigatório.")]
    [NoNumbers]
    public required string Name { get; set; }
    
    [Required(ErrorMessage = "O email é obrigatório.")]
    [EmailAddress(ErrorMessage = "Endereço de email inválido.")]
    public required string Email { get; set; }
    
    [Required(ErrorMessage = "A data de nascimento é obrigatória.")]
    [DataType(DataType.Date)]
    public required DateTime Birthday { get; set; }
}