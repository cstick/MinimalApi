using Serilog;
using System.Threading.RateLimiting;
using Web.Configurations;

namespace Web;

internal static class ComponentStartup
{
    /// <summary>
    /// Register rate limiting.
    /// </summary>
    /// <param name="app"></param>
    /// <returns></returns>
    public static WebApplicationBuilder AddRateLimits(this WebApplicationBuilder app)
    {
        app.Services.AddRateLimiter(limiterOptions =>
        {
            limiterOptions.RejectionStatusCode = StatusCodes.Status429TooManyRequests;

            var rateLimits = app.Configuration.GetValue(nameof(RateLimits), new RateLimits());

            foreach (var policy in rateLimits.Policies)
            {
                limiterOptions.AddPolicy(
                policyName: policy.PolicyName,
                partitioner: httpContext =>
                {
                    return RateLimitPartition.GetTokenBucketLimiter("Anon", _ =>
                        new TokenBucketRateLimiterOptions
                        {
                            TokenLimit = policy.TokenLimit,
                            QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
                            QueueLimit = policy.QueueLimit,
                            ReplenishmentPeriod = TimeSpan.FromSeconds(policy.ReplenishmentPeriod),
                            TokensPerPeriod = policy.TokensPerPeriod,
                            AutoReplenishment = true
                        });
                });
            }
        });

        return app;
    }

    public static WebApplicationBuilder AddLogging(this WebApplicationBuilder app)
    {
        app.Services.AddSerilog((services, loggerConfiguration) =>
            loggerConfiguration.ReadFrom.Configuration(app.Configuration));

        app.Services.AddHttpLogging(options =>
        {
            options.CombineLogs = true;
        });

        return app;
    }

    public static WebApplication AddLogging(this WebApplication app)
    {
        app.UseSerilogRequestLogging();
        app.UseHttpLogging();

        return app;
    }
}
