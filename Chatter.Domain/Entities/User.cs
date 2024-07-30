using Microsoft.AspNetCore.Identity;

namespace Chatter.Domain.Entities;

public class User : IdentityUser<Guid>
{
    public string? RefreshToken { get; set; }

    public DateTime? RefreshTokenExpiration { get; set; }
    
    public string? PhotoPath { get; set; }

    public List<Message> Messages { get; set; } = new List<Message>();
}