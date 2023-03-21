using static Application.Models.MultiLanguageModels;

namespace Application.Interfaces;

public interface IMessage
{
    string GetMessage(MessagesEnum stack);
}
