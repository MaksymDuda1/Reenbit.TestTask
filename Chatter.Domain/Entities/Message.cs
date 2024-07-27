using System.Text.Json.Serialization;
using Azure.AI.TextAnalytics;
using Chatter.Domain.Enums;

namespace Chatter.Domain.Entities;

public class Message
{
    public Guid Id { get; set; }

    public string Text { get; set; } = null!;
    
    public DateTime Time { get; set; }

    public Sentiment Sentiment { get; set; }

    public Guid UserId { get; set; }

    [JsonIgnore]
    public User? User { get; set; }
}