using Chatter.Application.Abstractions;
using Chatter.Application.Models;
using Chatter.Domain.Dtos;
using Chatter.Domain.Entities;
using Chatter.Domain.Enums;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Azure.SignalR.Management;

namespace Chatter.API.Hubs;

public class ChatHub : Hub
{
    private readonly IServiceManager serviceManager;
    private readonly IConfiguration configuration;
    private readonly IDictionary<string, string> connectedUsers;
    private readonly IMessageService messageService;
    private readonly ISentimentAnalysisService sentimentAnalysisService;
    private readonly IUserService userService;

    public ChatHub(
        IConfiguration configuration,
        IDictionary<string, string> connectedUsers,
        IMessageService messageService,
        ISentimentAnalysisService sentimentAnalysisService,
        IUserService userService)
    {
        this.configuration = configuration;
        this.connectedUsers = connectedUsers;
        this.messageService = messageService;
        this.sentimentAnalysisService = sentimentAnalysisService;
        this.userService = userService;
        serviceManager = new ServiceManagerBuilder()
            .WithOptions(option => { option.ConnectionString = this.configuration["AzureSignalR:ConnectionString"]; })
            .Build();
    }

    public async Task JoinChat(string userId)
    {
        var connectionId = Context.ConnectionId;
        if (connectedUsers.TryAdd(connectionId, userId))
        {
            await GetConnectedUsers();
            var loadedMessages = await messageService.LoadMessages();
            await Clients.Caller.SendAsync("LoadMessages", loadedMessages);
            
            var botMessage = new MessageDto()
            {
                Text = $"{userId} just joined our room",
                Time = DateTime.Now,
                Sentiment = Sentiment.Neutral,
                UserId = Guid.Empty,
                User = new UserDto(){UserName = "Chat bot"}
            };
            
            await Clients.All.SendAsync("UserJoined", botMessage);
        }
    }

    public async Task SendMessage(string message)
    {
        string connectionId = Context.ConnectionId;
        
        if (connectedUsers.TryGetValue(connectionId, out string userId))
        {
            var sentiment = sentimentAnalysisService.AnalyzeTheMessage(message);
            var parsedUserId = Guid.Parse(userId);
            var userMessage = new MessageDto()
            {
                Text = message,
                Time = DateTime.Now.ToUniversalTime(),
                Sentiment = sentiment,
                UserId = parsedUserId,
                User = await userService.GetUserById(parsedUserId)
            };
            
            await Clients.All.SendAsync("ReceiveMessage", userMessage);
            await messageService.SaveMessage(userMessage);
        }
    }

    public override async Task OnDisconnectedAsync(Exception ex)
    {
        string connectionId = Context.ConnectionId;
        if (connectedUsers.Remove(connectionId, out string userId))
        {
            var botMessage = new MessageDto()
            {
                Text = $"{userId} has left the chat",
                Time = DateTime.Now,
                Sentiment = Sentiment.Neutral,
                UserId = Guid.Empty,
                User = new UserDto(){UserName = "Chat bot"}
            };
            
            await Clients.All.SendAsync("ReceiveMessage", botMessage);
            await GetConnectedUsers();
            
            await base.OnDisconnectedAsync(ex);
        }
    }

    public async Task GetConnectedUsers()
    {
        var users = connectedUsers.Values.ToList();
        await Clients.All.SendAsync("ReceiveConnectedUsers", users);
    }
}