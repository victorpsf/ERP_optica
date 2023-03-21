using Application.Models.Security;
using static Application.Models.Security.ChipherModels;

namespace Application.Library;

public static class BinaryConverter
{
    public static string ToStringView(byte[] bytes, BinaryViewModels.BinaryView binType) => binType switch
    {
        BinaryViewModels.BinaryView.BASE64 => Convert.ToBase64String(bytes),
        BinaryViewModels.BinaryView.HEX => Convert.ToHexString(bytes),
        BinaryViewModels.BinaryView.BINARY => string.Join("", bytes.Select(a => a.ToString()).ToArray()),
        _ => throw new ArgumentException($"Error: Enum.StringView '{binType}' is not defined")
    };

    public static byte[] ToBytesView(string value, BinaryViewModels.BinaryView binType) => binType switch
    {
        BinaryViewModels.BinaryView.BASE64 => Convert.FromBase64String(value),
        BinaryViewModels.BinaryView.HEX => Convert.FromHexString(value),
        BinaryViewModels.BinaryView.BINARY => value.ToCharArray().Select(a => Convert.ToByte(a)).ToArray(),
        _ => throw new ArgumentException($"Error: Enum.StringView '{binType}' is not defined")
    };
}
