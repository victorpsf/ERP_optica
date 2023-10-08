namespace Application.Interfaces.Utils;

public interface IPrimitiveConverter
{
    public bool IsString(object value);
    public bool IsInt32(object value);
    public bool IsInt64(object value);
    public bool IsDecimal(object value);
    public bool IsDateTime(object value);

    public string GetString(object obj);
    public int GetInt32(object obj);
    public long GetInt64(object obj);
    public decimal GetDecimal(object obj);
    public DateTime GetDateTime(object obj);

    public object? ToType(Type type, object? value);
}
