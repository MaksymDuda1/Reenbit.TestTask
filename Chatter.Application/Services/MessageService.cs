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

        var mapped = messages.Select(mapper.Map<MessageDto>).ToList();
        return mapped;
    }

    public async Task SaveMessage(MessageDto userMessage)
    {
        await unitOfWork.Messages.InsertAsync(mapper.Map<Message>(userMessage));
        await unitOfWork.SaveAsync();
    }
}