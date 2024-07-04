namespace Web.Health;

internal class StartupBackgroundService(StartupHealthCheck healthCheck) : BackgroundService
{
    private readonly StartupHealthCheck _healthCheck = healthCheck;

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        // Simulate the effect of a long-running task.
        await Task.Delay(TimeSpan.FromSeconds(15), stoppingToken);

        _healthCheck.Started();
    }
}
