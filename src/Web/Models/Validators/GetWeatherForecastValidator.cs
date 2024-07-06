using FluentValidation;

namespace Web.Models.Validators;

/// <summary>
/// Validates <see cref="GetWeatherForecast"/>.
/// </summary>
public class GetWeatherForecastValidator : AbstractValidator<GetWeatherForecast>
{
    /// <summary>
    /// The validation rules.
    /// </summary>
    public GetWeatherForecastValidator()
    {
        RuleFor(m => m.Id)
            .NotEmpty();
    }
}