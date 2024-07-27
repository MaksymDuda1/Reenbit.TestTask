using Azure.AI.TextAnalytics;

namespace Chatter.Application.Abstractions;

public interface ISentimentAnalysisService
{
    TextSentiment AnalyzeTheMessage(string message);
}