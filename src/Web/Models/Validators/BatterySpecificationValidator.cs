using FluentValidation;

namespace Web.Models.Validators;

/// <summary>
/// Validates <see cref="IBatterySpecification"/>.
/// </summary>
public class BatterySpecificationValidator : AbstractValidator<IBatterySpecification>
{
    public BatterySpecificationValidator()
    {
        RuleFor(m => m.Voltage)
            .NotEmpty()
            .GreaterThan(1000);
    }
}
