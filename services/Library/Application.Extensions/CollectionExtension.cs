namespace Application.Extensions;

public static class CollectionExtension
{
    public static bool IsNotEmpty<T>(this IEnumerable<T> context) => (context is null) ? false : context.Any();
    public static bool IsEmpty<T>(this IEnumerable<T> context) => !IsNotEmpty(context);
}
