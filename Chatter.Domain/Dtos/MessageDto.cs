using System.Security.AccessControl;
using Chatter.Domain.Entities;
using Chatter.Domain.Enums;

namespace Chatter.Domain.Dtos;

public class MessageDto
{
    public string Text { get; set; } = null!;
    
    public DateTime Time { get; set; }

    public Sentiment Sentiment { get; set; }

    public Guid UserId { get; set; }

    public UserDto? User { get; set; }
}