using MediatR;
using Web.Models;

namespace Web.Handlers;

/// <summary>
/// Search weather forecasts.
/// </summary>
public class SearchWeatherHandler : IRequestHandler<SearchWeather, IEnumerable<WeatherForecast>>
{
    /// <inheritdoc/>
    public Task<IEnumerable<WeatherForecast>> Handle(
        SearchWeather request, 
        CancellationToken cancellationToken)
    {
        var forecast = new WeatherForecast
        {
            Id = Guid.NewGuid(),
            Location = request.Name,
        };
        IEnumerable<WeatherForecast> results =
        [
            forecast
        ];

        return Task.FromResult(results);
    }
}