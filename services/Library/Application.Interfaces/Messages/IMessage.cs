using static Application.Base.Models.MultiLanguageModels;

namespace Application.Interfaces.Messages;

public interface IMessage
{
    string GetMessage(MessagesEnum? stack);
    string GetMessage(string stack);
}

