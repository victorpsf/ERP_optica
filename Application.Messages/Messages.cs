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
        _ => new Ptbr(),
    };

    public string GetMessage(MessagesEnum stack)
    {
        try
        {
            PropertyInfo? propertie = null;
            string message = string.Empty;
            PropertyInfo[] properties = this.Language.GetType().GetProperties();

            foreach (PropertyInfo prop in properties)
            {
                if (prop.Name == stack.ToString())
                {
                    propertie = prop;
                    break;
                }
            }

            // || propertie?.GetValue(this.Language, null) is null
            if (propertie is null) throw new NotImplementedException();
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
