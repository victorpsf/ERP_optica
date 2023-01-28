using static Application.Library.Security.SecurityModels;

namespace Shared.Services
{
    public sealed class BinaryUtil
    {
        public static byte[] RandomBytes(int length)
        {
            byte[] bytes = new byte[length];
            var r = new Random();
            r.NextBytes(bytes);
            return bytes;
        }

        public static SecuritySaltWrited GetSaltBytes(string value)
        {
            int length = value.Length;
            length = (length < 50) ? length * 10 : length;

            decimal rest = 0;
            do
            {
                length++;
                rest = Convert.ToDecimal((length % 2));
            } while (rest != 0);


            return SecuritySalt.Write(RandomBytes(length));
        }
    }
}
