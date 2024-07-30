namespace Chatter.Application.Exceptions;

public class EntityNotFoundException : Exception
{
    public EntityNotFoundException(){}
    public EntityNotFoundException(string message){}

    public EntityNotFoundException(string message, Exception inner) : base(message, inner){}
}