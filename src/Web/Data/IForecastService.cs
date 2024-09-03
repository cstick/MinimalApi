using Web.Models;

namespace Web.Data
{
    /// <summary>
    /// Provides weather forecasts.
    /// </summary>
    public interface IForecastService
    {
        /// <summary>
        /// Add a forecast.
        /// </summary>
        /// <param name="value">The value to add.</param>
        /// <returns>The id assigned to the forecast.</returns>
        Guid Add(WeatherForecast value);

        /// <summary>
        /// Get a forecast by its id.
        /// </summary>
        /// <param name="id">The id of a forecast.</param>
        /// <returns>The <see cref="WeatherForecast"/> if found.</returns>
        WeatherForecast? Find(Guid id);
        
        /// <summary>
        /// Search forecasts.
        /// </summary>
        /// <param name="location">The location to filter on.</param>
        /// <param name="date">The date to filter on.</param>
        /// <returns>All forecasts found.</returns>
        IEnumerable<WeatherForecast> Search(string location, DateOnly? date);
    }
}