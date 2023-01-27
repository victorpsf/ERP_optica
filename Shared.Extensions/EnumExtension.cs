namespace Shared.Extensions
{
    public static class EnumExtension
    {
        public static int ToInt32(this Enum value) => (value is null) ? 0 : Convert.ToInt32(value);
    }
}
