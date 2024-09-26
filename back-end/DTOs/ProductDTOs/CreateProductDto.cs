using System.ComponentModel.DataAnnotations;

namespace back_end.DTOs.ProductDTOs;

public class CreateProductDto
{
    public string Name { get; set; }
    public double Price { get; set; }
}