using Application.Interfaces.Utils;

namespace Application.Utils;

public class PrimitiveConverter: IPrimitiveConverter
{
    public string GetString(object obj) => throw new NotImplementedException();
    public int GetInt32(object obj) => throw new NotImplementedException();
    public long GetLong64(object obj) => throw new NotImplementedException();
    public double GetDouble(object obj) => throw new NotImplementedException();
    public float GetFloat(object obj) => throw new NotImplementedException();
    public DateTime GetDateTime(object obj) => throw new NotImplementedException();
}
