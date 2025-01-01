using FluentValidation;

namespace Web.Models.Validators;

/// <summary>
/// Validate a <see cref="Battery"/>.
/// </summary>
public class BatteryValidator : AbstractValidator<Battery>
{
    /// <summary>
    /// Contruct the validator.
    /// </summary>
    /// <param name="batteryDefinitionValidator">A validator for the base class.</param>
    public BatteryValidator(
        BatteryDefinitionValidator batteryDefinitionValidator)
    {
        RuleFor(m => m.Name)
            .NotEmpty();

        Include(batteryDefinitionValidator);
    }
}
