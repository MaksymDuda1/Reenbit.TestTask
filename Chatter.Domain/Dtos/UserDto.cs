namespace Chatter.Domain.Dtos;

public class UserDto
{
    public Guid Id { get; set; }

    public string UserName { get; set; } = null!;
    
    public string? PhotoPath { get; set; }
}