using Application.Interfaces.Utils;
using System.Collections.Generic;

namespace Application.Services;

public class QueryStringReader<T> where T: class, new()
{
    private IPrimitiveConverter primitiveConverter;
    private readonly string queryString;
    private Dictionary<string, string?> readedData;

    public string QueryString
    { get => this.queryString; }

    public Dictionary<string, string?> ReadedData 
    { get => this.readedData; }

    public QueryStringReader(IPrimitiveConverter primitiveConverter, string queryString)
    {
        this.primitiveConverter = primitiveConverter;
        this.queryString = queryString;
        this.readedData = new Dictionary<string, string?>();
    }

    public B? GetInList<B>(List<B> values, int index)
    {
        if (values is null)
            return default(B);

        if (values.Count <= index)
            return default(B);

        return values[index];
    }

    private T GetInstanceModel()
    {
        var model = new T();

        foreach (string key in this.readedData.Keys)
        {
            var propertie = model.GetType().GetProperties().Where(a => a.Name.ToUpperInvariant() == key.ToUpperInvariant()).FirstOrDefault();
            if (propertie is null) 
                continue;

            propertie.SetValue(model, this.primitiveConverter.ToType(propertie.PropertyType, this.readedData[key]));
        }

        return model;
    }

    public T Decode ()
    {
        var queryParts = this.queryString.Substring(1).Split("&").ToList().Select(a => a.Split("=").ToList());
        foreach (List<string> part in queryParts)
        {
            string key = this.GetInList<string>(part, 0) ?? string.Empty;
            string? value = this.GetInList<string>(part, 1);

            if (string.IsNullOrEmpty(key)) continue;
            
            if (this.ReadedData.ContainsKey(key)) 
                this.readedData.Remove(key);

            this.ReadedData.Add(key, value);
        }

        return this.GetInstanceModel();
    }

    public static QueryStringReader<T> GetInstance(IPrimitiveConverter primitiveConverter, string queryString)
        => new QueryStringReader<T>(primitiveConverter, queryString);

    public static T Decode(IPrimitiveConverter primitiveConverter, string queryString)
        => GetInstance(primitiveConverter, queryString).Decode();
}
