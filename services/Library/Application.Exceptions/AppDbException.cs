namespace Application.Exceptions;

public class AppDbException: Exception
{
    public AppDbExceptionEnum Stack { get; }

    public AppDbException(AppDbExceptionEnum stack, string? message, Exception? innerException) : base(message, innerException)
    { this.Stack = stack; }

    public AppDbException(AppDbExceptionEnum stack, Exception? innerException) : this(stack, innerException?.Message, innerException)
    { }
}
