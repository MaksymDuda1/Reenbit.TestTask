namespace Chatter.Application.Exceptions;

public class CredentialValidationException : Exception
{
    public CredentialValidationException() {}
    public CredentialValidationException(string message){}
    public CredentialValidationException(string message, Exception inner) : base(message, inner){}

}

