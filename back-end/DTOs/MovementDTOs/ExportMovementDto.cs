using back_end.Models.Enums;

namespace back_end.DTOs.MovementDTOs;

public class ExportMovementDto
{
    public Guid Id { get; set; }
    public PaymentType PaymentType { get; set; }
    public double TotalValue { get; set; }
    public Guid UserId { get; set; }
    public string? UserName { get; set; }
    public bool IsBlocked { get; set; }
    public DateTime CreatedAt { get; set; }
    
    public List<ProductExportDto> Products { get; set; } = new List<ProductExportDto>();
    
    public class ProductExportDto
    {
        public Guid ProductId { get; set; }
        public string? ProductName { get; set; }
    }
}