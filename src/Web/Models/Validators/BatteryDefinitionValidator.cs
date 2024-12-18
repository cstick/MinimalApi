using FluentValidation;

namespace Web.Models.Validators;

public class BatteryDefinitionValidator : AbstractValidator<BatteryDefinition>
{
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