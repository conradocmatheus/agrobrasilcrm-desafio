﻿namespace back_end.Models.Entities;

public class User
{
    // Propriedades da classe User
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string? Email { get; set; } // {?} permite ser nullable ou opcional
    public DateOnly Birthday { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}