using back_end.Models.Enums;

namespace back_end.DTOs.MovementDTOs;

public class CreateMovementDto
{
    public PaymentType PaymentType { get; set; }
    public bool IsBlocked { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public Guid UserId { get; set; } // O ID do usuário associado
    public List<ProductMovementDto> Products { get; set; } // A lista de produtos
}

public class ProductMovementDto
{
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
}