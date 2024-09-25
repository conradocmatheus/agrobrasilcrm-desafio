using back_end.Models.Enums;

namespace back_end.DTOs.MovementDTOs;

public class CreateMovementDto
{
    public PaymentType PaymentType { get; set; }
    public bool IsBlocked { get; set; }
    public Guid UserId { get; set; } // O ID do usuário associado
    public List<CreateMovementProductDto> Products { get; set; } // A lista de produtos
}

public class CreateMovementProductDto
{
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
}