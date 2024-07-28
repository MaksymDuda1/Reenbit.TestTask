using AutoMapper;
using Chatter.Application.Abstractions;
using Chatter.Application.Exceptions;
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

        if (user == null)
            throw new EntityNotFoundException("User not found");
        
        return mapper.Map<UserDto>(user);
    }
}