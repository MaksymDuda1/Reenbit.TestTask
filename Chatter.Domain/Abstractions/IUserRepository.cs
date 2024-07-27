using System.Linq.Expressions;
using Chatter.Domain.Entities;

namespace Chatter.Domain.Abstractions;

public interface IUserRepository
{
    Task<User?> GetSingleByConditionAsync(
        Expression<Func<User, bool>> expression,
        params Expression<Func<User, object>>[] includes);
}