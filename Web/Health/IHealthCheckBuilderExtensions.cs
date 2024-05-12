using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Web.Health;

public static class IHealthCheckBuilderExtensions
{
    private static readonly TimeSpan DefaultFrequency = TimeSpan.FromSeconds(15);

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