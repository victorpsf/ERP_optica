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
}
