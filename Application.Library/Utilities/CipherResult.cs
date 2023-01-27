using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Library.Utilities
{
    public static class CipherResult
    {
        public class BinaryResult
        {
            public byte[] Binary { get; }

            public BinaryResult(byte[] binary)
            { this.Binary = binary; }

            public string Digest(BinaryConverter.StringView view)
                => BinaryConverter.ToStringView(this.Binary, view);
        }
    }
}
