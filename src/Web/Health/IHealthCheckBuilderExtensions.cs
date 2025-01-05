using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Web.Health;

internal static class IHealthCheckBuilderExtensions
{
    private static readonly TimeSpan DefaultFrequency = TimeSpan.FromSeconds(15);

    /// <summary>
    /// Add an API health check to the health check builder.
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="name">The name of the health check.</param>
    /// <param name="uri">The URI for checking connectivity with.</param>
    /// <param name="frequency">How frequently should the URI be checked.</param>
    /// <param name="failureStatus"></param>
    /// <param name="tags"></param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    public static IHealthChecksBuilder AddApiHealthCheck(
        this IHealthChecksBuilder builder,
        string name,
        Uri uri,
        TimeSpan? frequency = default,
        HealthStatus? failureStatus = default,
        IEnumerable<string>? tags = default)
    {
        builder.Services.TryAddTransient<ApiHealthCheck>();

        return builder.Add(new HealthCheckRegistration(
            name,
            sp =>
            {
                var healthCheck = sp.GetService<ApiHealthCheck>() ?? throw new InvalidOperationException("Health check is not registered.");
                return healthCheck
                    .WithClientUri(uri)
                    .WithFrequency(frequency ?? DefaultFrequency);
            },
            failureStatus,
            tags));
    }
}