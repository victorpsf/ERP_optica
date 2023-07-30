using Application.Base.Models;

namespace Application.Interfaces.Services;

public interface IAttributeValidationBase
{
    bool validate<T>(T model, out List<ControllerBaseModels.ValidationError> errors) where T : class, new();
}
