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
        public static IServiceCollection RegisterHealthChecks(this IServiceCollection services)
        {
            services
                .AddHealthChecks()
                .AddCheck<StartupHealthCheck>("Startup")
                .AddApiHealthCheck(
                    "Cat Facts",
                    new Uri("https://cat-fact.herokuapp.com/facts"),
                    frequency: TimeSpan.FromSeconds(15),
                    tags: ["Cat", "Dog"]);

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
