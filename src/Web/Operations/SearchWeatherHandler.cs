using MediatR;
using Web.Models;

namespace Web.Operations;

/// <summary>
/// Search weather forecasts.
/// </summary>
internal class SearchWeatherHandler : IRequestHandler<SearchWeather, IEnumerable<WeatherForecast>>
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
