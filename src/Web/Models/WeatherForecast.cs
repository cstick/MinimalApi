namespace Web.Models;

/// <summary>
/// The forecast for the weather ahead.
/// </summary>
public class WeatherForecast
{
    /// <summary>
    /// The id of the forecast.
    /// </summary>
    public required Guid Id { get; set; } = Guid.NewGuid();

    /// <summary>
    /// The location of the forecast.
    /// </summary>
    public required string Location { get; set; }

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
