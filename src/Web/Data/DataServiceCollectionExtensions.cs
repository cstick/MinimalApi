namespace Web.Data;

/// <summary>
/// Service collection extensions for the Data namespace.
/// </summary>
public static class DataServiceCollectionExtensions
{
    /// <summary>
    /// Add respositories to services.
    /// </summary>
    public static WebApplicationBuilder AddRepositories(this WebApplicationBuilder app)
    {
        app.Services.AddTransient<IBatteryRepository, BatteryRepository>();
        app.Services.AddTransient<IImageRepository, ImageRepository>();

        return app;
    }
}
