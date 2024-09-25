using back_end.Models.Enums;

namespace back_end.DTOs.MovementDTOs;

public class MovementDto
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public PaymentType PaymentType { get; set; }
    public bool IsBlocked { get; set; }
    public List<MovementProductDto> Products { get; set; } // Lista de produtos movimentados
}

public class MovementProductDto
{
    public Guid Id { get; set; } // Id do produto
    public int Quantity { get; set; }
}