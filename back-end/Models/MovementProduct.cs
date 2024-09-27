namespace back_end.Models;

public class MovementProduct
{
        // Propriedades da classe intermediária MovementProduct
        public Guid MovementId { get; set; }
        public Movement Movement { get; set; }

        public Guid ProductId { get; set; }
        public Product Product { get; set; }
        
        public int Quantity { get; set; }
}