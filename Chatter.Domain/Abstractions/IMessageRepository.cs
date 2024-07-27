using System.Linq.Expressions;
using Chatter.Domain.Entities;

namespace Chatter.Domain.Abstractions;

public interface IMessageRepository
{
    Task<List<Message>> GetAllAsync(
        params Expression<Func<Message, object>>[] includes);

    Task<List<Message>> GetByConditionsAsync(
        Expression<Func<Message, bool>> expression,
        params Expression<Func<Message, object>>[] includes);

    Task<Message?> GetSingleByConditionAsync(
        Expression<Func<Message, bool>> expression,
        params Expression<Func<Message, object>>[] includes);

    Task InsertAsync(Message entity);
    Task InsertRangeAsync(List<Message> entities);
    void Update(Message entity);
    Task Delete(Guid id);
    void Delete(Message entity);
}