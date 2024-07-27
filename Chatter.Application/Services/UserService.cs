using AutoMapper;
using Chatter.Application.Abstractions;
using Chatter.Domain.Abstractions;
using Chatter.Domain.Dtos;

namespace Chatter.Application.Services;

public class UserService(
    IUnitOfWork unitOfWork,
    IMapper mapper) : IUserService
{
    public async Task<UserDto> GetUserById(Guid userId)
    {
        var user = await unitOfWork.Users
            .GetSingleByConditionAsync(u => u.Id == userId);

        return mapper.Map<UserDto>(user);
    }
}