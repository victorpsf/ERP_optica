using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using static Application.Base.Models.MultiLanguageModels;

namespace Application.Messages;

public class MultiLanguage
{
    private static Regex Expression
    {
        get => new Regex("[A-Z]");
    }

    private static string ExtractExactLang(string lang)
        => string.Join(
                "",
                lang.ToUpperInvariant()
                    .ToCharArray()
                    .Where(x => Expression.IsMatch(x.ToString()))
                    .Select(x => x.ToString())
                    .ToArray()
            );

    public static LanguageEnum GetLanguage(string lang) => ExtractExactLang(lang) switch
    {
        "PTBR" => LanguageEnum.PTBR,
        _ => LanguageEnum.PTBR
    };
}
