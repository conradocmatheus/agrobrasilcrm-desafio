namespace back_end.Models;

public class Product
{
    // Propriedades da classe Product
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public int Quantity { get; set; }
    public double Price { get; set; }
    
    // Muitos produtos para muitos movimentos
    public IList<MovementProduct> MovementProducts { get; set; } = new List<MovementProduct>();
}