namespace Shared.Extensions
{
    public static class DateTimeExtension
    {
        public static bool InRangeYears(this DateTime? value, int range)
        {
            if (value is null) return false;
            return (value > DateTime.UtcNow.AddYears((range * -1)) && value < DateTime.UtcNow.AddYears(range));
        }

        public static bool IsLowerOrEqual(this DateTime? value, int range)
        {
            if (value is null) return false;
            return (value > DateTime.UtcNow.AddYears((range * -1)) && value <= DateTime.UtcNow);
        }
    }

}
