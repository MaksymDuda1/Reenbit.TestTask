using System.Security.Claims;
using Chatter.Application.Exceptions;
using Chatter.Application.Models;
using Chatter.Domain.Entities;

namespace Chatter.Application.Abstractions;

public interface ITokenService
{
    Task<string> RefreshToken(TokenApiModel token);
    Task RevokeTokenAsync(ClaimsPrincipal principal);
    string CreateAccessToken(ClaimsIdentity claims);
    string GenerateRefreshToken();
    ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    Task<ClaimsIdentity> GenerateClaims(User user);
    Task<TokenApiModel> GenerateToken(User user);

}