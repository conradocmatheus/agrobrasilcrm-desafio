using back_end.DTOs.ProductDTOs;
using back_end.Models.Enums;

namespace back_end.DTOs.MovementDTOs;

public class MovementDto
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public PaymentType PaymentType { get; set; }
    public bool IsBlocked { get; set; }
    public List<ProductDto> Products { get; set; } // Lista de produtos relacionados
}