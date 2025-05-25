using FluentValidation;
using Web.Models.Validators;

namespace Web.Operations;

/// <summary>
/// Validates a command to replace a battery.
/// </summary>
public class PutBatteryValidator : AbstractValidator<PutBattery>
{
    /// <param name="definitionValidator">Validates the battery definition.</param>
    public PutBatteryValidator(BatteryDefinitionValidator definitionValidator)
    {
        RuleFor(m => m.Name)
            .NotEmpty();

        RuleFor(m => m.Definition)
            .NotEmpty()
            .SetValidator(definitionValidator);
    }
}
