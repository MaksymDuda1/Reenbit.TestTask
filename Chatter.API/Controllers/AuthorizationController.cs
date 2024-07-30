using Chatter.Application.Abstractions;
using Chatter.Application.Exceptions;
using Chatter.Application.Models;
using Chatter.Domain.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Chatter.API.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthorizationController(IAuthorizationService authorizationService)
    : ControllerBase
{
    [HttpPost("login")]
    public async Task<ActionResult<TokenApiModel>> Login(LoginDto request)
    {
        return Ok(await authorizationService.Login(request));
    }

    [HttpPost("registration")]
    public async Task<IActionResult> Registration(RegistrationDto request)
    {
        return Ok(await authorizationService.Registration(request));
    }
}