using MediatR;

namespace Web.Models;

/// <summary>
/// Search criteria for weather forecasts.
/// </summary>
public class SearchWeather : IRequest<IEnumerable<WeatherForecast>>
{
    /// <summary>
    /// A name that shows up in the weather's summary.
    /// </summary>
    public string Name { get; set; } = string.Empty;
}
