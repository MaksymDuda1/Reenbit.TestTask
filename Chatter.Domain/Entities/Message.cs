using Azure.AI.TextAnalytics;

namespace Chatter.Domain.Entities;

public class Message
{
    public Guid Id { get; set; }

    public string Text { get; set; } = null!;
    
    public DateTime Time { get; set; }

    public TextSentiment Sentiment { get; set; }

    public Guid UserId { get; set; }

    public User? User { get; set; }
}