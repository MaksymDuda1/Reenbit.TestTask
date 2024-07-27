using Azure.AI.TextAnalytics;
using Chatter.Domain.Enums;

namespace Chatter.Application.Abstractions;

public interface ISentimentAnalysisService
{
    Sentiment AnalyzeTheMessage(string message);
} 