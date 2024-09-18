using back_end.Models.Enums;

namespace back_end.Models;

public class Transaction
{

    // Propriedades da classe Transaction
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public Product Product { get; set; }
    public User User { get; set; }
    public PaymentType PaymentType { get; set; }
    public bool IsBlocked { get; set; }
}