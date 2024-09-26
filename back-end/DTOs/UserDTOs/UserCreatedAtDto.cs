namespace back_end.DTOs.UserDTOs;

public class UserCreatedAtDto
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Email { get; set; }
    public DateTime Birthday { get; set; }
    public DateTime CreatedAt { get; set; }
}