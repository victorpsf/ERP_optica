using Application.Base.Models;
using Application.Interfaces.Services;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Application.Services;

public class AttributeValidationBase: IAttributeValidationBase
{
    private ILoggedUser loggedUser;

    public AttributeValidationBase(ILoggedUser loggedUser)
    {
        this.loggedUser = loggedUser;
    }

    public bool validate<T, B>(T model, ControllerBaseModels.RequestResult<B> output) where T : class, new()
    {
        foreach (PropertyInfo info in model.GetType().GetProperties())
            try
            {
                var results = new List<ValidationResult>();
                Validator.TryValidateProperty(info.GetValue(model), new ValidationContext(model) { MemberName = info.Name }, results);

                if (!results.Any()) 
                    continue;

                foreach (ValidationResult result in results)
                    output.addError(this.loggedUser.message.GetMessage(result.ErrorMessage ?? string.Empty), info.Name);
            }

            catch { }

        return !output.Failed;
    }
}
