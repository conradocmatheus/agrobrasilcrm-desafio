namespace back_end.Models;

public class User
{
    // Propriedades da classe User
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Email { get; set; }
    public DateTime Birthday { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    
    // Um para muitos, um usuário pode ter várias movimentações
    public IList<Movement> Movements { get; set; } = new List<Movement>();
}