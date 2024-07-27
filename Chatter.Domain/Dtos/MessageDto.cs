namespace Chatter.Domain.Dtos;

public class MessageDto
{
    public string Text { get; set; } = null!;
    
    public DateTime Time { get; set; }

    public Guid UserId { get; set; }
}