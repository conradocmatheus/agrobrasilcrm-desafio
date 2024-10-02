using back_end.Models.Enums;

namespace back_end.DTOs.MovementDTOs;

public class CreateMovementDto
{
    public PaymentType PaymentType { get; set; }
    public Guid UserId { get; set; }
    public List<CreateMovementProductDto> Products { get; set; }
}

public class CreateMovementProductDto
{
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
}