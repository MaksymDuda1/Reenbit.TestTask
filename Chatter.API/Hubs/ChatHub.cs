using Chatter.Application.Abstractions;
using Chatter.Application.Models;
using Chatter.Domain.Entities;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Azure.SignalR.Management;

namespace Chatter.API.Hubs;

public class ChatHub : Hub
{
    private readonly IServiceManager serviceManager;
    private readonly IConfiguration configuration;
    private readonly IServiceHubContext serviceHubContext;
    private readonly IDictionary<string, string> connectedUsers;
    private readonly IMessageService messageService;
    private readonly ISentimentAnalysisService sentimentAnalysisService;

    public ChatHub(
        IConfiguration configuration,
        IDictionary<string, string> connectedUsers,
        IMessageService messageService, ISentimentAnalysisService sentimentAnalysisService)
    {
        this.configuration = configuration;
        this.connectedUsers = connectedUsers;
        this.messageService = messageService;
        this.sentimentAnalysisService = sentimentAnalysisService;
        serviceManager = new ServiceManagerBuilder()
            .WithOptions(option => { option.ConnectionString = this.configuration["AzureSignalR:ConnectionString"]; })
            .Build();

        serviceHubContext = serviceManager.CreateHubContextAsync("ChatHub").Result;
    }

    public async Task JoinChat(string userId)
    {
        var connectionId = Context.ConnectionId;
        if (connectedUsers.TryAdd(connectionId, userId))
        {
            await GetConnectedUsers();
            var loadedMessages = await messageService.LoadMessages();
            await Clients.Caller.SendAsync("LoadMessages", loadedMessages);
            await Clients.All.SendAsync("UserJoined", "Chat bot", $"{userId} just joined our room");
        }
    }

    public async Task SendMessage(string message)
    {
        string connectionId = Context.ConnectionId;

        if (connectedUsers.TryGetValue(connectionId, out string userId))
        {
            await Clients.All.SendAsync("ReceiveMessage", userId, message, DateTime.Now);
        }

        var sentiment = sentimentAnalysisService.AnalyzeTheMessage(message);

        var userMessage = new UserMessage()
        {
            UserId = Guid.Parse(userId),
            Message = new Message
            {
                Text = message,
                Time = DateTime.Now,
                Sentiment = sentiment
            }
        };

        await messageService.SaveMessage(userMessage);
    }

    public override async Task OnDisconnectedAsync(Exception ex)
    {
        string connectionId = Context.ConnectionId;
        if (connectedUsers.Remove(connectionId, out string userId))
        {
            await Clients.All.SendAsync("ReceiveMessage", "Chat bot", $"{userId} has left the chat", DateTime.Now);
            await GetConnectedUsers();
        }

        await base.OnDisconnectedAsync(ex);
        Console.WriteLine($"Disconnected {connectionId}");
    }

    public async Task GetConnectedUsers()
    {
        var users = connectedUsers.Values.ToList();
        await Clients.All.SendAsync("ReceiveConnectedUsers", users);
    }

    public async Task DisconnectClient(string connectionId)
    {
        await serviceHubContext.Clients.Client(connectionId).SendAsync("Disconnect");
        Console.WriteLine($"Disconnected client: {connectionId}");
    }
}