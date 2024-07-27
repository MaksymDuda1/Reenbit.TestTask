using Chatter.Domain.Abstractions;

namespace Chatter.Domain.Abstractions;

public interface IUnitOfWork
{
    IUserRepository Users { get; }
    IMessageRepository Messages { get; }
    Task SaveAsync();
}