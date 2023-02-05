using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Messages;

public class MultiLanguage
{
    public static LanguageEnum GetLanguage(string lang) => lang switch
    {
        "pt-br" => LanguageEnum.PTBR,
        _ => LanguageEnum.PTBR
    };
}
