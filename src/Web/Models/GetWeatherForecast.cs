namespace Web.Models;

/// <summary>
/// A request for a weather forecast.
/// </summary>
public record GetWeatherForecast
{
    /// <summary>
    /// An id to be used as the summary.
    /// </summary>
    public string Id { get; set; } = string.Empty;
}
