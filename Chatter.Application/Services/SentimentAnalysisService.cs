using Azure;
using Azure.AI.TextAnalytics;
using Chatter.Application.Abstractions;

namespace Chatter.Application.Services;

public class SentimentAnalysisService : ISentimentAnalysisService
{
    private static string languageKey = "d86a9151758e42348cef1409416b61c7";

    private static string languageEndpoint =
        "https://sintementanalysisreenbittesttask.cognitiveservices.azure.com/";

    private static readonly AzureKeyCredential credentials = new AzureKeyCredential(languageKey);
    private static readonly Uri endpoint = new Uri(languageEndpoint);

    public TextSentiment AnalyzeTheMessage(string message)
    {
        var client = new TextAnalyticsClient(endpoint, credentials);
        var documents = new List<string> { message };

        AnalyzeSentimentResultCollection reviews = client.AnalyzeSentimentBatch(
            documents,
            options: new AnalyzeSentimentOptions { IncludeOpinionMining = true });

        var result = reviews.FirstOrDefault()?.DocumentSentiment.Sentiment ?? TextSentiment.Mixed;
        Console.WriteLine(result);
        return result;
    }
}