namespace Chatter.Domain.Dtos;

public class RegistrationDto
{
    public string Username { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;
}