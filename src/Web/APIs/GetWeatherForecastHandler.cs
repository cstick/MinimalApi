using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Web.Models;

namespace Web.APIs;

/// <summary>
/// Handles a request for a weather forecast.
/// </summary>
public class GetWeatherForecastHandler(IValidator<GetWeatherForecast> validator) : IRequestHandler<GetWeatherForecast, Results<Ok<WeatherForecast>, ValidationProblem>>
{
    /// <inheritdoc/>
    public async Task<Results<Ok<WeatherForecast>, ValidationProblem>> Handle(GetWeatherForecast request, CancellationToken cancellationToken)
    {
        var validation = await validator.ValidateAsync(request, cancellationToken);

        if (!validation.IsValid)
        {
            return TypedResults.ValidationProblem(validation.ToDictionary());
        }

        return TypedResults.Ok(new WeatherForecast
        {
            Id = Guid.NewGuid(),
            Location = request.Id,
            Summary = request.Id,
        });
    }
}
