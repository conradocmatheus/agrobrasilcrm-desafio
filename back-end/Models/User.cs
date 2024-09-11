namespace back_end.Models;

public class User
{
    // Propriedades da classe User
    public long Id { get; set; }
    public string Name { get; set; }
    public string? Email { get; set; }
    public DateOnly Birthday { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}