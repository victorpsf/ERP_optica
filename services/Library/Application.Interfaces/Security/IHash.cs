using static Application.Base.Models.BinaryManagerModels;
using static Application.Base.Models.SecurityModels;

namespace Application.Interfaces.Security;

public interface IHash
{
    string Update(string value, BinaryView outputFormat);
}
