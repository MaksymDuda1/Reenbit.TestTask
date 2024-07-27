using AutoMapper;
using Chatter.Application.Abstractions;
using Chatter.Application.Models;
using Chatter.Domain.Abstractions;
using Chatter.Domain.Dtos;
using Chatter.Domain.Entities;

namespace Chatter.Application.Services;

public class MessageService(
    IUnitOfWork unitOfWork,
    IMapper mapper) : IMessageService
{
    public async Task<List<MessageDto>> LoadMessages()
    {
        var messages = await unitOfWork.Messages
            .GetAllAsync(m => m.User);

        return messages.Select(mapper.Map<MessageDto>).ToList();
    }

    public async Task SaveMessage(UserMessage userMessage)
    {
        var message = new Message()
        {
            Text = userMessage.Message.Text,
            Time = userMessage.Message.Time.ToUniversalTime(),
            UserId = userMessage.UserId
            
        };
        
        await unitOfWork.Messages.InsertAsync(mapper.Map<Message>(message));
        await unitOfWork.SaveAsync();
    }
}