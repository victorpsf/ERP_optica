using System.ComponentModel.DataAnnotations;

namespace Application.Validations;

[AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
public sealed class DateTimeValidationAttribute: ValidationAttribute
{
    public int MinRange { get; set; }
    public int MaxRange { get; set; }
    public bool Required { get; set; }

    public override bool IsValid(object? value)
    {
        if (value is null)
            return this.Required ? false : true;

        DateTime now = DateTime.UtcNow;
        DateTime date = (DateTime) value;

        return date >= now.AddYears(this.MinRange) && date <= now.AddYears(this.MaxRange);
    }

    public override string FormatErrorMessage(string name) => this.ErrorMessage ?? string.Empty;
}
