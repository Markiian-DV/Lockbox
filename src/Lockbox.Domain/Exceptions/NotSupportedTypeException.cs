namespace Lockbox.Domain.Exceptions;

public class NotSupportedTypeException : Exception
{
    public NotSupportedTypeException()
    {
    }

    public NotSupportedTypeException(string message)
        : base(message)
    {
    }

    public NotSupportedTypeException(string message, Exception inner)
        : base(message, inner)
    {
    }
}