using Chatter.Domain.Dtos;

namespace Chatter.Application.Abstractions;

public interface IUserService
{
    Task<UserDto> GetUserById(Guid userId);
}