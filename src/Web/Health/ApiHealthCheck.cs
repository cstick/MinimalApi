using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Web.Health;

internal class ApiHealthCheck(IHttpClientFactory httpClientFactory) : IHealthCheck
{
    private bool _isHealthy = false;
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

            var response = await client.GetAsync(
                _uri,
                cancellationToken);

            _isHealthy = response.IsSuccessStatusCode;
            _lastHeartbeat = DateTime.UtcNow;
        }

        var data = new Dictionary<string, object>
        {
            { "checked", _lastHeartbeat }
        };

        if (_isHealthy)
        {
            return HealthCheckResult.Healthy("The client is healthy.", data);
        }

        return HealthCheckResult.Unhealthy("That client is unhealthy.", data: data);
    }
}