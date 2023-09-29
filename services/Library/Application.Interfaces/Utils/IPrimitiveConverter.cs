namespace Application.Interfaces.Utils;

public interface IPrimitiveConverter
{
    public string GetString(object obj);
    public int GetInt32(object obj);
    public long GetLong64(object obj);
    public double GetDouble(object obj);
    public float GetFloat(object obj);
    public DateTime GetDateTime(object obj);
}
