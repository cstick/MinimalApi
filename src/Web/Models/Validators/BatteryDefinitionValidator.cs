using FluentValidation;

namespace Web.Models.Validators;

/// <summary>
/// Validates a <see cref="BatteryDefinition"/>.
/// </summary>
public class BatteryDefinitionValidator : AbstractValidator<BatteryDefinition>
{
    /// <summary>
    /// Construct the validator.
    /// </summary>
    public BatteryDefinitionValidator()
    {
        RuleFor(m => m.AnsiName)
            .NotEmpty();

        RuleFor(m => m.IecName)
            .NotEmpty();

        RuleFor(m => m.Voltage)
            .NotEmpty();
    }
}