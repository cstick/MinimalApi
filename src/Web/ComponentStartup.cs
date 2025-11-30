using Serilog;
using System.Diagnostics;
using System.Threading.RateLimiting;
using Web.Configurations;
using Web.Swagger;

namespace Web;

internal static class ComponentStartup
{
    /// <summary>
    /// Register rate limiting.
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    public static WebApplicationBuilder AddRateLimits(this WebApplicationBuilder builder)
    {
        builder.Services.AddRateLimiter(limiterOptions =>
        {
            limiterOptions.RejectionStatusCode = StatusCodes.Status429TooManyRequests;

            var rateLimits = builder.Configuration.GetValue(nameof(RateLimits), new RateLimits());

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

        return builder;
    }

    public static WebApplicationBuilder AddLogging(this WebApplicationBuilder builder)
    {
        if (builder.Environment.IsDevelopment())
        {
            Serilog.Debugging.SelfLog.Enable(msg => Debug.WriteLine(msg));
        }

        builder.Services.AddSerilog((services, loggerConfiguration) =>
            loggerConfiguration.ReadFrom.Configuration(builder.Configuration));

        builder.Services.AddHttpLogging(options =>
        {
            options.CombineLogs = true;
        });

        return builder;
    }

    public static WebApplication UseLogging(this WebApplication app)
    {
        app.UseSerilogRequestLogging();
        app.UseHttpLogging();

        return app;
    }

    public static WebApplicationBuilder AddSwagger(this WebApplicationBuilder builder)
    {
        builder.Services.AddSwaggerGen(setup =>
        {
            setup.SwaggerDoc("v1", new()
            {
                Version = "1",
                Title = "Product",
                Description = "An API created to becaome familiar with miminal APIs."
            });

            setup.SwaggerDoc("v2", new()
            {
                Version = "2",
                Title = "Product",
                Description = "An API created to becaome familiar with miminal APIs."
            });

            // Ensure deprecated ApiDescriptions are reflected in OpenAPI
            setup.OperationFilter<DeprecatedOperationFilter>();
        });

        return builder;
    }

    public static WebApplication MapSwagger(this WebApplication app)
    {
        app.UseSwagger();

        app.UseSwaggerUI(options =>
        {
            var descriptions = app.DescribeApiVersions();

            foreach (var description in descriptions)
            {
                options.SwaggerEndpoint(
                    $"/swagger/{description.GroupName}/swagger.json",
                    description.GroupName.ToUpperInvariant());
            }

            options.EnableTryItOutByDefault();
            options.EnableDeepLinking();
            options.RoutePrefix = "api";
            options.DisplayRequestDuration();
        });

        return app;
    }
}
