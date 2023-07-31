namespace Application.Exceptions;

public class BusinessException: Exception
{
    public BusinessExceptionEnum? Stack { get; }

    public BusinessException(string? message, Exception? innerException, BusinessExceptionEnum? stack) : base(message, innerException)
    { this.Stack = stack; }

    public BusinessException(Exception? innerException, BusinessExceptionEnum? stack) : this(innerException?.Message, innerException, stack)
    { }

    public BusinessException(string message) : this(message, null, null)
    { }
}
