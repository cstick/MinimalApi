using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Web.Health;

internal class ApiHealthCheck(
    IHttpClientFactory httpClientFactory,
    ILogger<ApiHealthCheck> logger) : IHealthCheck
{
    private readonly object _lock = new();
    private volatile bool _isHealthy = false;
    private DateTime _lastHeartbeat = DateTime.MinValue.ToUniversalTime();

    public Uri? _uri;
    public TimeSpan _frequency = TimeSpan.FromSeconds(15);

    public ApiHealthCheck WithClientUri(Uri uri)
    {
        _uri = uri;

        return this;
    }

    public ApiHealthCheck WithFrequency(TimeSpan frequency)
    {
        _frequency = frequency;

        return this;
    }

    public async Task<HealthCheckResult> CheckHealthAsync(
        HealthCheckContext context,
        CancellationToken cancellationToken = default)
    {

        if (_lastHeartbeat <= DateTime.UtcNow.Subtract(_frequency))
        {
            var client = httpClientFactory.CreateClient();
            client.Timeout = TimeSpan.FromSeconds(3);

            try
            {
                var response = await client.GetAsync(
                    _uri,
                    cancellationToken);

                lock (_lock)
                {
                    _isHealthy = response.IsSuccessStatusCode;
                }

                _lastHeartbeat = DateTime.UtcNow;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Health check failed.");

                lock (_lock)
                {
                    _isHealthy = false;
                }
            }
        }

        var data = new Dictionary<string, object>
        {
            { "checked", _lastHeartbeat }
        };

        return _isHealthy
            ? HealthCheckResult.Healthy("The client is healthy.", data)
            : HealthCheckResult.Unhealthy("The client is unhealthy.", data: data);
    }
}