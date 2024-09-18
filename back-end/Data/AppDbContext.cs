using back_end.Models;
using Microsoft.EntityFrameworkCore;

namespace back_end.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Transaction> Transactions { get; set; }
    public DbSet<Product> Products { get; set; }


}