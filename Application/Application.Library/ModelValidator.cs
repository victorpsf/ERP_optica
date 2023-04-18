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
    protected IAppLogger logger;
    protected T? model;

    private ModelValidator(T model, IMessage message, IAppLogger logger, List<AppValidate> rules)
    {
        this.model = model;
        this.message = message;
        this.rules = rules;
        this.logger = logger;
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

    private void MinRule(AppValidationInfo info, AppValidateRule rule, object? value)
    {
        if (value is null) return;

        try
        {
            switch(value.GetType().Name)
            {
                case "Int32":
                    if (Convert.ToInt32(value) <= rule.value)
                        this.AddError(info.Attribute, this.message.GetMessage(rule.Stack));
                    break;
                case "Int64":
                    if (Convert.ToInt64(value) <= rule.value)
                        this.AddError(info.Attribute, this.message.GetMessage(rule.Stack));
                    break;
                case "String":
                    if ((Convert.ToString(value) ?? string.Empty).Length <= rule.value)
                        this.AddError(info.Attribute, this.message.GetMessage(rule.Stack));
                    break;
            }
        }

        catch (Exception ex)
        {
            this.logger.Error("ModelValidator.MinRule", ex);
            this.AddError(info.Attribute, this.message.GetMessage(rule.Stack));
        }
    }

    private void MaxRule(AppValidationInfo info, AppValidateRule rule, object? value)
    {
        if (value is null) return;

        try
        {
            switch (value.GetType().Name)
            {
                case "Int32":
                    if (Convert.ToInt32(value) <= rule.value)
                        this.AddError(info.Attribute, this.message.GetMessage(rule.Stack));
                    break;
                case "Int64":
                    if (Convert.ToInt64(value) <= rule.value)
                        this.AddError(info.Attribute, this.message.GetMessage(rule.Stack));
                    break;
                case "String":
                    if ((Convert.ToString(value) ?? string.Empty).Length <= rule.value)
                        this.AddError(info.Attribute, this.message.GetMessage(rule.Stack));
                    break;
            }
        }

        catch (Exception ex)
        {
            this.logger.Error("ModelValidator.MinRule", ex);
            this.AddError(info.Attribute, this.message.GetMessage(rule.Stack));
        }
    }

    public AppValidationResult Validate(AppValidate[] rules)
    {
        this.SetProperties();
        this.LoadInformations();

        foreach (var info in this.informations)
        {
            var validateRules = rules.Where(a => a.Attribute.ToUpperInvariant() == info.Attribute.ToUpperInvariant());

            if (!validateRules.Any())
                continue;

            var validation = validateRules.First();

            var required = validation.Rule.Where(a => a.Rule == AppValidateRuleEnum.Required);
            if (required.Any() && info.Value is null)
            {
                this.AddError(info.Attribute, this.message.GetMessage(required.First().Stack));
                continue;
            }

            var min = validation.Rule.Where(a => a.Rule == AppValidateRuleEnum.Min);
            var max = validation.Rule.Where(a => a.Rule == AppValidateRuleEnum.Min);

            if (min.Any())
                this.MinRule(info, min.First(), info.Value);
            if (max.Any())
                this.MaxRule(info, min.First(), info.Value);
        }

        return new AppValidationResult(this.erros);
    }

    public static ModelValidator<B> GetInstance<B>(B model, IMessage message, IAppLogger logger, List<AppValidate> rules) where B: class
        => new ModelValidator<B>(model, message, logger, rules);
}
