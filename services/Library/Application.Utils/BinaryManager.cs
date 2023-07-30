using static Application.Base.Models.BinaryManagerModels;

namespace Application.Utils;

public class BinaryManager
{
    private byte[] binary;

    private BinaryManager(byte[] binary)
    { this.binary = binary; }

    public byte[] Binary { get { return this.binary; } }

    public string ToString(BinaryView encode) => ToStringView(this.binary, encode);
    public override string ToString() => ToString(BinaryView.String);

    public static string ToStringView(byte[] binary, BinaryView encode) => encode switch
    {
        BinaryView.String => string.Join("", binary.Select(a => Convert.ToChar(a)).Select(a => a.ToString()).ToArray()),
        BinaryView.Base64 => Convert.ToBase64String(binary),
        BinaryView.Hexadecimal => Convert.ToHexString(binary),
        _ => string.Empty
    };

    public static byte[] ToByteArray(string view, BinaryView encode) => encode switch
    {
        BinaryView.String => view.ToCharArray().Select(a => Convert.ToByte(a)).ToArray(),
        BinaryView.Base64 => Convert.FromBase64String(view),
        BinaryView.Hexadecimal => Convert.FromHexString(view),
        _ => new byte[] { }
    };

    public static BinaryManager From(byte[] binary) => new BinaryManager(binary);
    public static BinaryManager From(string value, BinaryView encode) => From(ToByteArray(value, encode));
    public static BinaryManager From(string value) => From(value, BinaryView.String);
}
