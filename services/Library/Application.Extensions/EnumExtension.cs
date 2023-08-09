namespace Application.Extensions;

public static class EnumExtension
{
    public static int intValue(this Enum value)
        => value is null ? 0: Convert.ToInt32(value);
}
