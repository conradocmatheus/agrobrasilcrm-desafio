using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using back_end.CustomActionFilters;

namespace back_end.DTOs.ProductDTOs;

public class CreateProductDto
{
    [Required(ErrorMessage = "O nome é obrigatório.")]
    [NoNumbers]
    public string? Name { get; set; }
    
    [Required(ErrorMessage = "O preço é obrigatório.")]
    [Range(0.01, double.MaxValue, ErrorMessage = "O preço deve ser maior que 0.01.")]
    public double Price { get; set; } = 0.01;

    [DefaultValue(1)]
    [Range(0, int.MaxValue, ErrorMessage = "Quantidade não pode ser menor que 0")]
    public int Quantity { get; set; }
}