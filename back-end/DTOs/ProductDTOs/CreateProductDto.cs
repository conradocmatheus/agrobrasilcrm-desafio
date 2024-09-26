using System.ComponentModel.DataAnnotations;
using back_end.CustomActionFilters;

namespace back_end.DTOs.ProductDTOs;

public class CreateProductDto
{
    [Required(ErrorMessage = "O nome é obrigatório.")]
    [NoNumbers]
    public string Name { get; set; }
    
    [Required(ErrorMessage = "Preco e obrigatorio.")]
    public double Price { get; set; }
}