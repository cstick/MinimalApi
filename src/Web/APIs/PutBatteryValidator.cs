using FluentValidation;
using Web.Models.Validators;

namespace Web.APIs;

public class PutBatteryValidator : AbstractValidator<PutBattery>
{
    public PutBatteryValidator(BatterySpecificationValidator specificationValidator)
    {
        RuleFor(m => m.Name)
            .NotEmpty();

        RuleFor(m => m.Specification)
            .NotEmpty()
            .SetValidator(specificationValidator);
    }
}
