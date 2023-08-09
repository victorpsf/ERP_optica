using Application.Base.Models;

namespace Application.Exceptions;

public class AppDbException: Exception
{
    public MultiLanguageModels.MessagesEnum? Stack { get; }

    public AppDbException(MultiLanguageModels.MessagesEnum? stack, string? message, Exception? innerException) : base(message, innerException)
    { this.Stack = stack; }

    public AppDbException(MultiLanguageModels.MessagesEnum? stack, Exception? innerException) : this(stack, innerException?.Message, innerException)
    { }

    public AppDbException(MultiLanguageModels.MessagesEnum? stack): this(stack, null)
    { }
}
