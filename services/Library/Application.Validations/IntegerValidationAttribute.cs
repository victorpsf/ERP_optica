using System.ComponentModel.DataAnnotations;

namespace Application.Validations;

[AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
public sealed class IntegerValidationAttribute: ValidationAttribute
{
    public long Min { get; set; }
    public long Max { get; set; }
    public bool Required { get; set; }

    public override bool IsValid(object? value)
    {
        if (value is null)
            return this.Required ? false : true;

        var data = Convert.ToInt64(value);

        if (Min > 0 && data < Min) 
            return false;

        if (Max > 0 && data > Max)
            return false;

        return true;
    }

    public override string FormatErrorMessage(string name) => this.ErrorMessage ?? string.Empty;
}
