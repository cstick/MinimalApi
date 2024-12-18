using FluentValidation;
using Web.Models.Validators;

namespace Web.APIs;

public class PutBatteryValidator : AbstractValidator<PutBattery>
{
    public PutBatteryValidator(BatteryDefinitionValidator definitionValidator)
    {
        RuleFor(m => m.Name)
            .NotEmpty();

        RuleFor(m => m.Definition)
            .NotEmpty()
            .SetValidator(definitionValidator);
    }
}
