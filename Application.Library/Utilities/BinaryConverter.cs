namespace Application.Library.Utilities
{
    public partial class BinaryConverter
    {
        public static string ToStringView(byte[] bytes, StringView binType) => binType switch
        {
            StringView.BASE64 => Convert.ToBase64String(bytes),
            StringView.HEXADECIMAL => Convert.ToHexString(bytes),
            StringView.BINARY => string.Join("", bytes.Select(a => a.ToString()).ToArray()),
            _ => throw new ArgumentException($"Error: Enum.StringView '{binType}' is not defined")
        };

        public static byte[] ToBytesView(string value, StringView binType) => binType switch
        {
            StringView.BASE64 => Convert.FromBase64String(value),
            StringView.HEXADECIMAL => Convert.FromHexString(value),
            StringView.BINARY => value.ToCharArray().Select(a => Convert.ToByte(a)).ToArray(),
            _ => throw new ArgumentException($"Error: Enum.StringView '{binType}' is not defined")
        };
    }
}
