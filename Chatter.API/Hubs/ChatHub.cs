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
    private readonly IDictionary<string, Guid> connectedUsers;
    private readonly IMessageService messageService;
    private readonly ISentimentAnalysisService sentimentAnalysisService;
    private readonly IUserService userService;

    public ChatHub(
        IConfiguration configuration,
        IDictionary<string, Guid> connectedUsers,
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

    public async Task JoinChat(Guid userId)
    {
        if (connectedUsers.TryAdd(Context.ConnectionId, userId))
        {
            var user = await userService.GetUserById(userId);
            await GetConnectedUsers();
            
            var loadedMessages = await messageService.LoadMessages();
            await Clients.Caller.SendAsync("LoadMessages", loadedMessages);

            var botMessage = new MessageDto()
            {
                Text = $"{user.UserName} just joined our room",
                Time = DateTime.Now,
                Sentiment = Sentiment.Neutral,
                UserId = Guid.Empty,
                User = new UserDto() { UserName = "Chat bot" }
            };

            await Clients.All.SendAsync("UserJoined", botMessage);
        }
    }

    public async Task SendMessage(string message)
    {
        if (connectedUsers.TryGetValue(Context.ConnectionId, out Guid userId))
        {
            var sentiment = sentimentAnalysisService.AnalyzeTheMessage(message);
            var userMessage = new MessageDto()
            {
                Text = message,
                Time = DateTime.Now.ToUniversalTime(),
                Sentiment = sentiment,
                UserId = userId,
                User = await userService.GetUserById(userId)
            };

            await Clients.All.SendAsync("ReceiveMessage", userMessage);
            await messageService.SaveMessage(userMessage);
        }
    }

    public override async Task OnDisconnectedAsync(Exception ex)
    {
        if (connectedUsers.Remove(Context.ConnectionId, out Guid userId))
        {
            var user = await userService.GetUserById(userId);

            var botMessage = new MessageDto()
            {
                Text = $"{user.UserName} has left the chat",
                Time = DateTime.Now,
                Sentiment = Sentiment.Neutral,
                UserId = Guid.Empty,
                User = new UserDto() { UserName = "Chat bot" }
            };

            await Clients.All.SendAsync("ReceiveMessage", botMessage);
            await GetConnectedUsers();

            await base.OnDisconnectedAsync(ex);
        }
    }

    public async Task GetConnectedUsers()
    {
        var userIds = connectedUsers.Values.ToList();
        var users = new List<UserDto>();
        foreach (var userId in userIds)
        {
            users.Add(await userService.GetUserById(userId));
        }
        
        await Clients.All.SendAsync("ReceiveConnectedUsers", users);
    }
}