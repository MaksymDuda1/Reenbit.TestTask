using Chatter.Application.Abstractions;
using Chatter.Application.Exceptions;
using Chatter.Domain.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Chatter.API.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthorizationController(IAuthorizationService authorizationService, IMessageService messageService)
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

    [HttpGet]
    public async Task<IActionResult> Logout()
    {
        var messages = await messageService.LoadMessages();
        return Ok(messages);
    }
}