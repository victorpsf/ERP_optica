using Application.Interfaces.Utils;

namespace Application.Utils;

public class PrimitiveConverter: IPrimitiveConverter
{
    public bool IsString(object value) => value.GetType() == typeof(string);
    public bool IsInt32(object value) => value.GetType() == typeof(int);
    public bool IsInt64(object value) => value.GetType() == typeof(long);
    public bool IsDecimal(object value) => value.GetType() == typeof(decimal);
    public bool IsDateTime(object value) => value.GetType() == typeof(DateTime);

    public string GetString(object obj)
    {
        try
        {
            if (this.IsString(obj)) return (string)obj;
            else if (this.IsInt64(obj)) return Convert.ToString(obj) ?? string.Empty;
            else if (this.IsDecimal(obj)) return Convert.ToString(obj) ?? string.Empty;
            else if (this.IsInt32(obj)) return Convert.ToString(obj) ?? string.Empty;
            else return string.Empty;
        }

        catch { return string.Empty; }
    }

    public int GetInt32(object obj) {
        try
        {
            if (this.IsInt32(obj)) return (int)obj;
            else if (this.IsInt64(obj)) return Convert.ToInt32(obj);
            else if (this.IsDecimal(obj)) return Convert.ToInt32(obj);
            else if (this.IsString(obj)) return Convert.ToInt32(obj);
            else return 0;
        }

        catch { return 0; }
    }

    public long GetInt64(object obj)
    {
        try
        {
            if (this.IsInt64(obj)) return (long)obj;
            else if (this.IsInt32(obj)) return Convert.ToInt64(obj);
            else if (this.IsDecimal(obj)) return Convert.ToInt64(obj);
            else if (this.IsString(obj)) return Convert.ToInt64(obj);
            else return 0;
        }

        catch { return 0; }
    }

    public decimal GetDecimal(object obj)
    {
        try
        {
            if (this.IsDecimal(obj)) return (decimal)obj;
            else if (this.IsInt64(obj)) return Convert.ToDecimal(obj);
            else if (this.IsInt32(obj)) return Convert.ToDecimal(obj);
            else if (this.IsString(obj)) return Convert.ToDecimal(obj);
            else return 0;
        }

        catch { return 0; }
    }

    public DateTime GetDateTime(object obj)
    {
        try
        {
            if (this.IsDecimal(obj)) return DateTime.UtcNow;
            else if (this.IsInt64(obj)) return DateTimeOffset.FromUnixTimeMilliseconds((long)obj).DateTime;
            else if (this.IsInt32(obj)) return DateTimeOffset.FromUnixTimeMilliseconds((int)obj).DateTime;
            else if (this.IsString(obj)) return DateTimeMatch.extract((string)obj);
            else if (this.IsDateTime(obj)) return (DateTime)obj;
            else return DateTime.UtcNow;
        }

        catch { return DateTime.UtcNow; }
    }

    public object? ToType(Type type, object? value)
    {
        if (value is null) 
            return null;

        if      (type == typeof(string))    return this.GetString(value);
        else if (type == typeof(int))       return this.GetInt32(value);
        else if (type == typeof(long))      return this.GetInt64(value);
        else if (type == typeof(decimal))   return this.GetDecimal(value);
        else if (type == typeof(DateTime))  return this.GetDateTime(value);
        else                                return null;
    }
}
