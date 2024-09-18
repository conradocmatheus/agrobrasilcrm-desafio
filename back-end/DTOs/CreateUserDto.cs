namespace back_end.DTOs;

public class CreateUserDto
{
    public string Name { get; set; }
    public string Email { get; set; }
    public DateTime Birthday { get; set; }
}