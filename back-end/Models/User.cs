namespace back_end.Models;

public class User
{
    // Propriedades da classe User
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Email { get; set; }
    public DateTime Birthday { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
    
    // Um para muitos, um usuário pode ter várias movimentações
    public ICollection<Movement> Movements { get; set; } = new List<Movement>();
}