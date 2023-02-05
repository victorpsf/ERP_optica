using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Application.Messages;

public class Messages
{
    private LanguageEnum Lang { get; }
    private MultiLanguage Language { get; }

    private Messages(LanguageEnum lang)
    { 
        this.Lang = lang;
        this.Language = this.GetLang();
    }

    private MultiLanguage GetLang() => this.Lang switch
    {
        LanguageEnum.PTBR => new Ptbr(),
        _ => new MultiLanguage(),
    };

    public string GetMessage(MessagesEnum stack)
    {
        try
        {
            PropertyInfo? propertie = null;
            string message = string.Empty;

            foreach (PropertyInfo prop in this.Language.GetType().GetProperties())
            {
                if (prop.Name == stack.ToString())
                {
                    propertie = prop;
                    break;
                }
            }

            if (propertie != null || propertie?.GetValue(this.Language, null) is null) throw new NotImplementedException();
            message = propertie?.GetValue(this.Language, null).ToString();
            if (message is null) throw new NotImplementedException();
            return message;
        }

        catch
        {
            return "Não foi possível processa sua requisição";
        }
    }

    public static Messages Create(LanguageEnum lang) => new Messages(lang);
}
