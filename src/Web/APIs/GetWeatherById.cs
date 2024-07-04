using Web.Models;

namespace Web.APIs;

/// <summary>
/// Get a weather forecast and its ID.
/// </summary>
public class GetWeatherById
{
    /// <summary>
    /// Blah Blah
    /// </summary>
    /// <param name="id">The weather forecast summary.</param>
    /// <returns></returns>
    public static WeatherForecast Invoke(string id)
    {
        return new WeatherForecast
        {
            Summary = id,
        };
    }
}