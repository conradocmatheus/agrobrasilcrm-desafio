using System.ComponentModel.DataAnnotations;
using System.Numerics;
using back_end.CustomActionFilters;

namespace back_end.DTOs.ProductDTOs;

public class UpdateProductDto
{
    [Required(ErrorMessage = "O nome é obrigatório.")]
    [NoNumbers]
    public string? Name { get; set; }

    [Required(ErrorMessage = "O preço é obrigatório.")]
    [Range(0.01, double.MaxValue, ErrorMessage = "O preço deve ser maior que 0.01.")]
    public double Price { get; set; }

    [Range(0, double.MaxValue, ErrorMessage = "A quantidade deve ser maior ou igual a 0.")]
    public int Quantity { get; set; } = 0;
}