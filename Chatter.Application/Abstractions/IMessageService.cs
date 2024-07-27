using Chatter.Application.Models;
using Chatter.Domain.Dtos;

namespace Chatter.Application.Abstractions;

public interface IMessageService
{
    Task<List<MessageDto>> LoadMessages();

    Task SaveMessage(MessageDto userMessage);
}