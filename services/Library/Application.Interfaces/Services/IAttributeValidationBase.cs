using Application.Base.Models;

namespace Application.Interfaces.Services;

public interface IAttributeValidationBase
{
    bool validate<T, B>(T model, ControllerBaseModels.RequestResult<B> output) where T : class, new();
}
