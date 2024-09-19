namespace back_end.DTOs;

public class UserCreatedAtDto
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Email { get; set; }
    public DateTime Birthday { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
}