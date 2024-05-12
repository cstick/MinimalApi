using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Web.Health;

public class StartupHealthCheck : IHealthCheck
{
    private volatile bool _isStarted;

    public void Started()
    {
        _isStarted = true;
    }

    public Task<HealthCheckResult> CheckHealthAsync(
        HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        if (_isStarted)
        {
            return Task.FromResult(HealthCheckResult.Healthy("The startup task has completed."));
        }

        return Task.FromResult(HealthCheckResult.Unhealthy("That startup task is still running."));
    }
}
