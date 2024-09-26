using back_end.Models.Enums;

namespace back_end.DTOs.MovementDTOs;

public class GetAllMovementsDto
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public PaymentType PaymentType { get; set; }
    public double TotalValue { get; set; }
    public bool IsBlocked { get; set; }
    public Guid UserId { get; set; }
    public List<Guid> MovementProductIds { get; set; } = new List<Guid>();
}