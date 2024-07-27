using Chatter.Domain.Abstractions;
using Chatter.Domain.Entities;

namespace Chatter.Infrastructure.Repositories;

public class UserRepository(ChatterDbContext context)
    : BaseRepository<User>(context), IUserRepository
{
}