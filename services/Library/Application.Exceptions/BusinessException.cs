using Application.Base.Models;

namespace Application.Exceptions;

public class BusinessException: Exception
{
    public MultiLanguageModels.MessagesEnum? Stack { get; }

    public BusinessException(string? message, Exception? innerException, MultiLanguageModels.MessagesEnum? stack) : base(message, innerException)
    { this.Stack = stack; }

    public BusinessException(Exception? innerException, MultiLanguageModels.MessagesEnum? stack) : this(innerException?.Message, innerException, stack)
    { }

    public BusinessException(MultiLanguageModels.MessagesEnum stack) : this(null, stack)
    { }
}
