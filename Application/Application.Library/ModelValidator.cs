using Application.Interfaces;
using System.Reflection;
using static Application.Models.Controller.ValidationModels;

namespace Application.Library;

public class ModelValidator<T> where T : class
{
    protected List<PropertyInfo> properties = new List<PropertyInfo>();
    protected List<AppValidationInfo> informations = new List<AppValidationInfo>();
    protected List<AppError> erros = new List<AppError>();
    protected List<AppValidate> rules = new List<AppValidate>();
    protected IMessage message;
    protected T? model;

    private ModelValidator(T model, IMessage message, List<AppValidate> rules)
    {
        this.model = model;
        this.message = message;
        this.rules = rules;
    }

    private void SetProperties()
        => this.properties = new List<PropertyInfo>(this.model?.GetType().GetProperties() ?? Array.Empty<PropertyInfo>());

    private void LoadInformations()
        => this.informations.AddRange(
            this.properties.Select(info => new AppValidationInfo
            {
                Info = info,
                Attribute = info.Name,
                Value = info.GetValue(this.model, null)
            })
        );

    private void AddError(string attribute, string message) 
        => this.erros.Add(new AppError { Attribute = attribute, Message = message });

    public AppValidationResult Validate(params AppValidate[] rules)
    {
        this.SetProperties();
        this.LoadInformations();

        foreach (var info in this.informations)
        {
            var validateRules = rules.Where(a => a.Attribute.ToUpperInvariant() == info.Attribute.ToLowerInvariant());

            if (!validateRules.Any())
                continue;

            var validation = validateRules.First();

            var required = validation.Rule.Where(a => a.Rule == AppValidateRuleEnum.Required);
            if (required.Any() && info.Value is null)
            {
                this.AddError(info.Attribute, this.message.GetMessage(required.First().Stack));
                continue;
            }

            // [TO-DO] :: CRIAR REGRA DE MIN E MAX
        }

        return new AppValidationResult();
    }

    public static ModelValidator<B> GetInstance<B>(B model, IMessage message, List<AppValidate> rules) where B: class
        => new ModelValidator<B>(model, message, rules);
}
