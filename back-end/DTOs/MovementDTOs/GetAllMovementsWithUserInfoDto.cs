using back_end.Models.Enums;

namespace back_end.DTOs.MovementDTOs;

public class GetAllMovementsWithUserInfoDto
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public PaymentType PaymentType { get; set; }
    public bool IsBlocked { get; set; }
    public Guid UserId { get; set; }
    public UserInfoDto User { get; set; }
    public List<Guid> MovementProductIds { get; set; } = new List<Guid>();
}

public class UserInfoDto
{
    public string Name { get; set; }
    public string Email { get; set; }
    public DateTime Birthday { get; set; }
}