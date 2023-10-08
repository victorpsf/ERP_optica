using System.ComponentModel.DataAnnotations;

namespace Application.Validations;

[AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
public sealed class StringValidationAttribute: ValidationAttribute
{
    public int MinLength { get; set; }
    public int MaxLength { get; set; }
    public bool Required { get; set; }

    public override bool IsValid(object? value)
    {
        if (value is null)
            return this.Required ? false : true;

        var data = (string)value;

        return data.Length >= this.MinLength && data.Length < this.MaxLength;
    }

    public override string FormatErrorMessage(string name) => this.ErrorMessage ?? string.Empty;
}
