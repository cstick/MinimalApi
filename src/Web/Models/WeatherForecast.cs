namespace Web.Models;

/// <summary>
/// The forecast for the weather ahead.
/// </summary>
public class WeatherForecast
{
    /// <summary>
    /// The date of the forecast.
    /// </summary>
    public DateOnly Date { get; set; }

    /// <summary>
    /// The temperature in celsius.
    /// </summary>
    public int TemperatureC { get; set; }

    /// <summary>
    /// The temperature in fahrenheit.
    /// </summary>
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

    /// <summary>
    /// A summarized description.
    /// </summary>
    public string? Summary { get; set; }
}
