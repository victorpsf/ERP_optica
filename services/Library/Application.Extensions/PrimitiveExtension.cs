using System.Text.RegularExpressions;

namespace Application.Extensions;

public static class PrimitiveExtension
{
    public static object? Get<T>(this List<T> values, int index)
    {
        if (values is null) 
            return null;

        if (values.Count < index)
            return null;

        return values[index];
    }

    public static string ToIntString(this string value)
    {
        if (value is null) return "0";
        return string.Join("", value.ToCharArray().Select(a => a.ToString()).Where(a => { try { return new Regex("\\d").IsMatch(a); } catch { return false; } }).ToArray());
    }

    public static int[] ToIntArray(this string value) => value.ToIntString().ToCharArray().Select(a => Convert.ToInt32(a.ToString())).ToArray();

    public static int[] Slice(this int[] values, int start, int count)
    {
        if (values is null) 
            return new int[0];

        return values.Skip(start).Take(count).ToArray();
    }

    public static string ToLikeSql(this string value)
    {
        if (value is null) 
            return string.Empty;

        return string.Join("%", value.Split(" ").Select(a => Regex.Replace(a, "/([^a-zA-Z]*)", "%")).ToArray());
    }

    public static Boolean BeforeRange(this DateTime value, int min) => DateTime.UtcNow.AddYears(min) >= value;
    public static Boolean AfterRange(this DateTime value, int max) => DateTime.UtcNow.AddYears(max) <= value;

    public static Boolean InRange(this DateTime value, int min, int max)
    {
        return BeforeRange(value, min) && AfterRange(value, max);
    }
}
