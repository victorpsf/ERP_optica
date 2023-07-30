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

    public string GetMessage(MessagesEnum stack)
    {
        try
        {
            var results = this.Language.GetType()
                .GetProperties()
                .Where(a => a.Name.ToUpperInvariant() == stack.ToString().ToUpperInvariant())
                .Select(a => a.GetValue(this.Language, null))
                .Where(a => a is not null)
                .Select(a => a?.ToString() ?? string.Empty)
                .Where(a => !string.IsNullOrEmpty(a));

            if (!results.Any()) throw new NotImplementedException();
            return results.FirstOrDefault() ?? string.Empty;
        }

        catch
        { return "Não foi possível processa sua requisição"; }
    }
}
