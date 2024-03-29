﻿using Application.Base.Models;
using Application.Extensions;
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

    public bool validate<T, B>(T model, ControllerBaseModels.RequestResult<B> output, int? Index = null) where T : class, new()
    {
        try
        {
            if (model is null)
            {
                output.addError(this.loggedUser.message.GetMessage("ERROR_INPUT_NULLABLE"), "INPUT_ERROR");
                throw new ValidationException();
            }

            foreach (PropertyInfo info in model.GetType().GetProperties())
            {
                var results = new List<ValidationResult>();
                Validator.TryValidateProperty(info.GetValue(model), new ValidationContext(model) { MemberName = info.Name }, results);

                if (!results.Any()) 
                    continue;

                foreach (ValidationResult result in results)
                    output.addError(this.loggedUser.message.GetMessage(result.ErrorMessage ?? string.Empty), info.Name, Index);
            }

        }
        catch { output.addError("Internal server error", ""); }

        return output.Failed;
    }

    public bool validate<T, B>(List<T> models, ControllerBaseModels.RequestResult<B> output) where T : class, new()
    {
        for (int i = 0; i < models.Count; i++)
            this.validate(models.Get(i), output, i);

        return output.Failed;
    }
}
