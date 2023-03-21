namespace Application.Models.Security;

public static class ChipherModels
{
    public class BinaryResult { 
        public byte[] Bytes { get; } = new byte[0];
        public BinaryViewModels.BinaryView Format { get; }

        private BinaryResult(byte[] bytes, BinaryViewModels.BinaryView view)
        { 
            this.Bytes = bytes;
            this.Format = view;
        }

        public static BinaryResult Create(byte[] bytes, BinaryViewModels.BinaryView view) => new BinaryResult(bytes, view);
    }
}
