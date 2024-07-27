namespace Chatter.Application.Exceptions;

public class TokenApiModel
{
    public string? AccessToken { get; set; }
    public string? RefreshToken { get; set; }
}