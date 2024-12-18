using FluentValidation;

namespace Web.Models.Validators;

public class BatteryValidator : AbstractValidator<Battery>
{
    public BatteryValidator(
        BatteryDefinitionValidator batteryDefinitionValidator)
    {
        RuleFor(m => m.Name)
            .NotEmpty();

        Include(batteryDefinitionValidator);
    }
}
