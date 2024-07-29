using Chatter.Application.Exceptions;
using Chatter.Application.Models;
using Chatter.Domain.Dtos;
using Microsoft.AspNetCore.Identity;

namespace Chatter.Application.Abstractions;

public interface IAuthorizationService
{
    Task<TokenApiModel> Login(LoginDto loginDto);
    Task<TokenApiModel> Registration(RegistrationDto registrationDto);
    Task Logout();
}