namespace Application.Utils;
using System.Text.RegularExpressions;

public class PatternData
{
    public Regex regex { get; set; } = new Regex(".*");
    public string pattern { get; set; } = string.Empty;
}

public class DateTimePattern
{
    private static RegexOptions option = RegexOptions.ECMAScript | RegexOptions.IgnoreCase;

    public static List<PatternData> getPatterns() => new List<PatternData>()
    {
        new PatternData { regex = new Regex("\\d{4}(\\-\\d{2}){2}T\\d{2}(:\\d{2}){2}\\.\\d{1,}Z", option), pattern = "yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff'Z'" },

        new PatternData { regex = new Regex("\\d{4}(\\-\\d{2}){2}\\s\\d{2}(\\:\\d{2}){2}.\\d{1,}", option), pattern = "yyyy'-'MM'-'dd HH':'mm':'ss'.'fff" },
        new PatternData { regex = new Regex("\\d{4}(\\-\\d{2}){2}\\s\\d{2}(\\:\\d{2}){2}", option), pattern = "yyyy'-'MM'-'dd HH':'mm':'ss" },
        new PatternData { regex = new Regex("\\d{4}(\\-\\d{2}){2}\\s\\d{2}\\:\\d{2}", option), pattern = "yyyy'-'MM'-'dd HH':'mm" },
        new PatternData { regex = new Regex("\\d{4}(\\-\\d{2}){2}", option), pattern = "yyyy'-'MM'-'dd" },

        new PatternData { regex = new Regex("\\d{4}(\\/\\d{2}){2}\\s\\d{2}(\\:\\d{2}){2}.\\d{1,}", option), pattern = "yyyy'/'MM'/'dd HH':'mm':'ss'.'fff" },
        new PatternData { regex = new Regex("\\d{4}(\\/\\d{2}){2}\\s\\d{2}(\\:\\d{2}){2}", option), pattern = "yyyy'/'MM'/'dd HH':'mm':'ss" },
        new PatternData { regex = new Regex("\\d{4}(\\/\\d{2}){2}\\s\\d{2}\\:\\d{2}", option), pattern = "yyyy'/'MM'/'dd HH':'mm" },
        new PatternData { regex = new Regex("\\d{4}(\\/\\d{2}){2}", option), pattern = "yyyy'/'MM'/'dd" },

        new PatternData { regex = new Regex("(\\d{2}\\-){2}\\d{4}\\s\\d{2}(\\:\\d{2}){2}.\\d{1,}", option), pattern = "dd'-'MM'-'yyyy HH':'mm':'ss'.'fff" },
        new PatternData { regex = new Regex("(\\d{2}\\-){2}\\d{4}\\s\\d{2}(\\:\\d{2}){2}", option), pattern = "dd'-'MM'-'yyyy HH':'mm':'ss" },
        new PatternData { regex = new Regex("(\\d{2}\\-){2}\\d{4}\\s\\d{2}\\:\\d{2}", option), pattern = "dd'-'MM'-'yyyy HH':'mm" },
        new PatternData { regex = new Regex("(\\d{2}\\-){2}\\d{4}", option), pattern = "dd'-'MM'-'yyyy" },

        new PatternData { regex = new Regex("(\\d{2}\\/){2}\\d{4}\\s\\d{2}(\\:\\d{2}){2}.\\d{1,}", option), pattern = "dd'/'MM'/'yyyy HH':'mm':'ss'.'fff" },
        new PatternData { regex = new Regex("(\\d{2}\\/){2}\\d{4}\\s\\d{2}(\\:\\d{2}){2}", option), pattern = "dd'/'MM'/'yyyy HH':'mm':'ss" },
        new PatternData { regex = new Regex("(\\d{2}\\/){2}\\d{4}\\s\\d{2}\\:\\d{2}", option), pattern = "dd'/'MM'/'yyyy HH':'mm" },
        new PatternData { regex = new Regex("(\\d{2}\\/){2}\\d{4}", option), pattern = "dd'/'MM'/'yyyy" }
    };
}
