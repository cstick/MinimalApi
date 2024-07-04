using Web.Models;

namespace Web.APIs;

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