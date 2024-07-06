using FluentValidation;

namespace Web.Models.Validators;

public class GetWeatherByIdValidator : AbstractValidator<GetWeatherById>
{
    public GetWeatherByIdValidator()
    {
        RuleFor(m => m.Id)
            .NotEmpty();
    }
}