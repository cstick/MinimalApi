using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Web.Models;
using Web.Models.Validators;

namespace Web.Operations;

/// <summary>
/// Handles a request for a weather forecast.
/// </summary>
public class GetWeatherHandler(GetWeatherForecastValidator validator) : IRequestHandler<GetWeather, Results<Ok<WeatherForecast>, ValidationProblem>>
{
    /// <inheritdoc/>
    public async Task<Results<Ok<WeatherForecast>, ValidationProblem>> Handle(GetWeather request, CancellationToken cancellationToken)
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
