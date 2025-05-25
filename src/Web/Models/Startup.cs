using Web.Models.Validators;

namespace Web.Models;

/// <summary>
/// Startup extensions for models.
/// </summary>
internal static class Startup
{
    /// <summary>
    /// Register domain models.
    /// </summary>
    /// <param name="app"></param>
    /// <returns></returns>
    public static WebApplicationBuilder AddModels(this WebApplicationBuilder app)
    {
        app.Services.AddTransient<BatteryDefinitionValidator>();
        app.Services.AddTransient<BatteryValidator>();
        app.Services.AddTransient<GetWeatherForecastValidator>();

        return app;
    }
}
