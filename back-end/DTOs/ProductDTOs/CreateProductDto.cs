using System.ComponentModel.DataAnnotations;

namespace back_end.DTOs;

public class CreateProductDto
{
    [MinLength(1, ErrorMessage = "Nome não pode ser vazio")]
    [MaxLength(200, ErrorMessage = "Não ultrapasse 200 caracteres")]
    public string Name { get; set; }
    public double Price { get; set; }
}