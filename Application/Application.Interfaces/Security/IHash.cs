using static Application.Models.Security.HashModels;

namespace Application.Interfaces.Security;

public interface IHash
{
    IHash Create(AppHashAlgorithm cipher);
    byte[] Update(byte[] value);
    byte[] Update(string value);
}
