using Chatter.Domain.Abstractions;
using Chatter.Domain.Entities;

namespace Chatter.Infrastructure.Repositories;

public class MessageRepository(ChatterDbContext context)
    : BaseRepository<Message>(context), IMessageRepository
{
}