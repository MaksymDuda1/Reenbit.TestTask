using Azure;
using Azure.AI.TextAnalytics;
using Chatter.Application.Abstractions;
using Chatter.Domain.Enums;
using Microsoft.Extensions.Configuration;

namespace Chatter.Application.Services;

public class SentimentAnalysisService : ISentimentAnalysisService
{
    private readonly string languageKey;
    private readonly string languageEndpoint;
    private readonly AzureKeyCredential credentials;
    private readonly Uri endpoint;

    public SentimentAnalysisService(IConfiguration configuration)
    {
        languageKey = configuration["AzureCognitiveService:Key"];
        languageEndpoint = configuration["AzureCognitiveService:Endpoint"];

        if (string.IsNullOrEmpty(languageKey) || string.IsNullOrEmpty(languageEndpoint))
        {
            throw new ArgumentNullException("Azure Cognitive Service key or endpoint is missing in configuration.");
        }

        credentials = new AzureKeyCredential(languageKey);
        endpoint = new Uri(languageEndpoint);
    }

    public Sentiment AnalyzeTheMessage(string message)
    {
        var client = new TextAnalyticsClient(endpoint, credentials);
        var documents = new List<string> { message };

        AnalyzeSentimentResultCollection reviews = 
            client.AnalyzeSentimentBatch(documents, options: new AnalyzeSentimentOptions { IncludeOpinionMining = true });

        var result = reviews.FirstOrDefault()?.DocumentSentiment.Sentiment;

        return result switch
        {
            TextSentiment.Negative => Sentiment.Negative,
            TextSentiment.Positive => Sentiment.Positive,
            TextSentiment.Neutral => Sentiment.Neutral,
            TextSentiment.Mixed => Sentiment.Mixed,
            _ => Sentiment.Mixed
        };
    }
}