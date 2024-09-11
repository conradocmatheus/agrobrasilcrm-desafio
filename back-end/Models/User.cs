namespace back_end.Models;

public class User
{
    // Propriedades da classe User
    private long Id { get; set; }
    private string Name { get; set; }
    private string? Email { get; set; } // {?} permite ser nullable ou opcional
    private DateOnly Birthday { get; set; }
    private DateTime CreatedAt { get; set; }
    private DateTime UpdatedAt { get; set; }
}