﻿using back_end.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace back_end.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
}