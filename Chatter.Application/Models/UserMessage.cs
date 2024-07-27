using Chatter.Domain.Entities;

namespace Chatter.Application.Models;

public class UserMessage
{
    public Guid UserId { get; set; }

    public Message Message { get; set; } = null!;
}