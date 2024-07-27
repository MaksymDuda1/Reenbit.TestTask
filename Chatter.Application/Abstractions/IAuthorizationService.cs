using Chatter.Application.Exceptions;
using Chatter.Domain.Dtos;
using Microsoft.AspNetCore.Identity;

namespace Chatter.Application.Abstractions;

public interface IAuthorizationService
{
    Task<TokenApiModel> Login(LoginDto loginDto);
    Task<IdentityResult> Registration(RegistrationDto registrationDto);
    Task Logout();
}