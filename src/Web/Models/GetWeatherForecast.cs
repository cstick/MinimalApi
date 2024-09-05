using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Web.Models;

/// <summary>
/// A request for a weather forecast.
/// </summary>
public record GetWeatherForecast : IRequest<Results<Ok<WeatherForecast>, ValidationProblem>>
{
    /// <summary>
    /// An id to be used as the summary.
    /// </summary>
    public string Id { get; set; } = string.Empty;
}
