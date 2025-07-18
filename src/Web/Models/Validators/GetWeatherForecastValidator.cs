﻿using FluentValidation;

namespace Web.Models.Validators;

/// <summary>
/// Validates <see cref="GetWeather"/>.
/// </summary>
internal class GetWeatherForecastValidator : AbstractValidator<GetWeather>
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
