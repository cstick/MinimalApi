namespace Web.Data;

/// <summary>
/// Service collection extensions for the Data namespace.
/// </summary>
public static class DataServiceCollectionExtensions
{
    /// <summary>
    /// Add respositories to services.
    /// </summary>
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddTransient<IBatteryRepository, BatteryRepository>();

        return services;
    }
}
