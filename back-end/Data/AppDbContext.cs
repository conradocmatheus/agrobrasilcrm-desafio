using back_end.Models;
using Microsoft.EntityFrameworkCore;

namespace back_end.Data;

public class AppDbContext(DbContextOptions options) : DbContext(options)
{
    // Define os DbSets para as classes
    public DbSet<User> Users { get; set; }
    public DbSet<Movement> Movements { get; set; }
    public DbSet<Product> Products { get; set; }
    
    // Tabela intermediaria no relacionamento n-n entre movement e products
    public DbSet<MovementProduct> MovementProducts { get; set; }

    // Método para configurar os relacionamentos e outras configurações entre as tabelas
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configura a entidade MovementProduct
        modelBuilder.Entity<MovementProduct>()
            .HasKey(mp => new { mp.MovementId, mp.ProductId });

        // Configura o relacionamento entre MovementProduct e Movement
        modelBuilder.Entity<MovementProduct>()
            .HasOne(mp => mp.Movement) // Cada linha em MovementProduct se refere a uma movimentação
            .WithMany(m => m.MovementProducts) // Um movimento pode ter vários produtos
            .HasForeignKey(mp => mp.MovementId); // Define que a chave estrangeira será MovementId

        // Configura o relacionamento entre MovementProduct e Product
        modelBuilder.Entity<MovementProduct>()
            .HasOne(mp => mp.Product) // Cada registro em MovementProduct se refere a um único produto
            .WithMany(p => p.MovementProducts) // Um produto pode fazer parte de várias movimentações
            .HasForeignKey(mp => mp.ProductId); // Define que a chave estrangeira será ProductId
        
        // Configurando o relacionamento entre Movement e User
        modelBuilder.Entity<Movement>()
            .HasOne(m => m.User) // Cada registro em Movement tem um usuário
            .WithMany(u => u.Movements) // Um usuário pode ter várias Movement
            .HasForeignKey(m => m.UserId); // Define que a chave estrangeira será UserId
    }
}