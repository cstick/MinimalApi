namespace Web.Health
{
    /// <summary>
    /// Extensions for startup.
    /// </summary>
    public static class HealthCheckExtensions
    {
        /// <summary>
        /// Register health checks.
        /// </summary>
        /// <param name="services">A <see cref="IServiceCollection"/> to register with.</param>
        public static IServiceCollection AddApplicationHealthChecks(this IServiceCollection services)
        {
            services.AddHostedService<StartupBackgroundService>();
            services.AddSingleton<StartupHealthCheck>();
            services.AddSingleton<ApiHealthCheck>();

            services
                .AddHealthChecks()
                .AddCheck<StartupHealthCheck>("Startup")
                .AddApiHealthCheck(
                    "Google",
                    new Uri("https://google.com"),
                    frequency: TimeSpan.FromSeconds(15),
                    tags: ["Search", "Alive"]);

            return services;
        }

        /// <summary>
        /// Map health check endpoints.
        /// </summary>
        public static WebApplication MapProductHealthChecks(this WebApplication app)
        {
            app.MapHealthChecks("/health");

            app.MapHealthChecks("/health/ready", new()
            {
                ResponseWriter = HealthResponse.Writer
            });

            return app;
        }
    }
}
