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
            .LessThanOrEqualTo(0)
            .GreaterThan(1000);
    }
}