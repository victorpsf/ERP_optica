using System.ComponentModel.DataAnnotations;

namespace Application.Validations;

[AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
public sealed class EnumValidationAttribute: ValidationAttribute
{
    public bool Required { get; set; }
    public Type Enumerable { get; set; }

    public override bool IsValid(object? value)
    {
        if (value is null)
            return (this.Required) ? false : true;

        if (!this.Enumerable.IsEnum)
            return false;

        var values = Enum.GetValues(this.Enumerable);
        var data = (int)value;
        var enumerables = new List<int>();

        foreach (var v in values) enumerables.Add(Convert.ToInt32(v));

        return enumerables.Contains(data);
    }

    public override string FormatErrorMessage(string name) => this.ErrorMessage ?? string.Empty;
}
