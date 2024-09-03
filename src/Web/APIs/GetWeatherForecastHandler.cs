using FluentValidation;
using Microsoft.AspNetCore.Http.HttpResults;
using Web.Models;

namespace Web.APIs;

/// <summary>
/// Handles a request for a weather forecast.
/// </summary>
public class GetWeatherForecastHandler(IValidator<GetWeatherForecast> validator)
{
    /// <summary>
    /// Gets a weather forecast based on the request.
    /// </summary>
    /// <param name="request">Parameters for finding the forecast.</param>
    /// <returns></returns>
    public Results<Ok<WeatherForecast>, ValidationProblem> Invoke(GetWeatherForecast request)
    {
        var validation = validator.Validate(request);

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
