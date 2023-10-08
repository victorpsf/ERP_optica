using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Application.Utils
{
    public class DateTimeMatch
    {
        public static DateTime extract(string date)
        {
            foreach (var pattern in DateTimePattern.getPatterns())
                try
                {
                    if (!pattern.regex.IsMatch(date)) continue;
                    return DateTime.ParseExact(date, pattern.pattern, CultureInfo.InvariantCulture);
                }

                catch { continue; }

            return DateTime.UtcNow;
        }
    }
}
