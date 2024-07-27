using System.Linq.Expressions;
using Chatter.Domain.Entities;

namespace Chatter.Domain.Abstractions;

public interface IMessageRepository
{
    Task<List<Message>> GetAllAsync(
        params Expression<Func<Message, object>>[] includes);
    
    Task InsertAsync(Message entity);
    
}