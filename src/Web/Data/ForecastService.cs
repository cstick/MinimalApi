using Web.Models;

namespace Web.Data;

/// <inheritdoc/>
public class ForecastService : IForecastService
{
    private readonly IList<WeatherForecast> forecasts = [];

    /// <inheritdoc/>
    public Guid Add(WeatherForecast value)
    {
        forecasts.Add(value);

        return value.Id;
    }

    /// <inheritdoc/>
    public WeatherForecast? Find(Guid id)
    {
        return forecasts.FirstOrDefault(forecast => Equals(id, forecast.Id));
    }

    /// <inheritdoc/>
    public IEnumerable<WeatherForecast> Search(string location, DateOnly? date)
    {
        var results = forecasts.AsQueryable();

        if (!string.IsNullOrEmpty(location))
        {
            results = results.Where(forecast => string.Equals(location, forecast.Location, StringComparison.OrdinalIgnoreCase));
        }

        if (date is not null)
        {
            results = results.Where(forecast => Equals(date, forecast.Date));
        }

        return results;
    }
}
