using static Application.Models.MultiLanguageModels;

namespace Application.Mensagens;

public class MultiLanguage
{
    public static LanguageEnum GetLanguage(string lang) => lang.ToUpperInvariant() switch
    {
        "PT-BR" => LanguageEnum.PTBR,
        "PTBR" => LanguageEnum.PTBR,
        "BR" => LanguageEnum.PTBR,
        _ => LanguageEnum.PTBR
    };
}