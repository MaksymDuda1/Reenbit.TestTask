using Chatter.Domain.Abstractions;

namespace Chatter.Infrastructure.Repositories;

public class UnitOfWork(
    ChatterDbContext context,
    Lazy<IUserRepository> userRepository,
    Lazy<IMessageRepository> messageRepository) : IUnitOfWork
{
    public IUserRepository Users => userRepository.Value;
    
    public IMessageRepository Messages => messageRepository.Value;
    
    public async Task SaveAsync() => await context.SaveChangesAsync();

}