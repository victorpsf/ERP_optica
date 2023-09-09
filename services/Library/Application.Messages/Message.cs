using Application.Interfaces.Messages;
using Application.Messages.Messages;
using static Application.Base.Models.MultiLanguageModels;

namespace Application.Messages;

public class Message: IMessage
{
    private LanguageEnum Lang { get; }
    private MultiLanguage Language { get; }

    private Message(LanguageEnum lang)
    {
        this.Lang = lang;
        this.Language = this.GetLang();
    }

    private MultiLanguage GetLang() => this.Lang switch
    {
        LanguageEnum.PTBR => new Ptbr(),
        _ => new Ptbr()
    };

    public static Message Create(LanguageEnum lang) => new Message(lang);

    public string GetMessage(MessagesEnum? stack) => GetMessage(stack.ToString() ?? string.Empty);

    private string? ConverToString(Object? value)
    {
        try
        { return Convert.ToString(value); } 

        catch 
        { return null; }
    }

    public string GetMessage(string stack)
    {
        try
        {
            var messages = this.Language.GetType()
                .GetProperties()
                .Where(a => a.Name.ToUpperInvariant() == stack.ToUpperInvariant())
                .Select(a => a.GetValue(this.Language, null))
                .Select(a => this.ConverToString(a))
                .Where(a => !string.IsNullOrEmpty(a))
                .ToList();

            if (!messages.Any()) 
                throw new NotImplementedException();

            return messages.FirstOrDefault() ?? string.Empty;
        }

        catch
        { return "Não foi possível processa sua requisição"; }
    }
}
