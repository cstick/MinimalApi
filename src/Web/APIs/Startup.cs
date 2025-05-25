using Asp.Versioning.Conventions;
using Microsoft.AspNetCore.HttpLogging;
using Web.APIs.Groups;
using Web.Operations;

namespace Web.APIs;

internal static class Startup
{
    /// <summary>
    /// Add API dependencies.
    /// </summary>
    /// <param name="app"></param>
    /// <returns></returns>
    public static WebApplicationBuilder AddAPI(this WebApplicationBuilder app)
    {
        app.Services.AddTransient<PutBatteryValidator>();
        app.Services.AddTransient<CreateBatteryRequestHandler>();

        return app;
    }

    /// <summary>
    /// Add API endpoints.
    /// </summary>
    /// <param name="app"></param>
    /// <param name="configure"></param>
    /// <returns></returns>
    public static IEndpointRouteBuilder AddAPI(
        this WebApplication app,
        Action<APIConfiguration>? configure = default)
    {
        var configuration = new APIConfiguration();

        if (configure is not null)
        {
            configure(configuration);
        }

        var versionSet = app
            .NewApiVersionSet()
            .HasApiVersion(2)
            .HasDeprecatedApiVersion(1)
            .ReportApiVersions()
            .Build();

        var apiGroup = app
            .MapGroup("/product/v{version:apiVersion}")
            .RequireRateLimiting(configuration.RateLimitertPolicyName);

        apiGroup
            .MapGroup("/batteries")
            .WithTags("Batteries")
            .WithHttpLogging(HttpLoggingFields.All)
            .MapBatteryApi()
            .WithApiVersionSet(versionSet)
            .MapToApiVersion(2);

        apiGroup
            .MapGroup("batteries/{name}/images")
            .WithTags("Images")
            .WithHttpLogging(HttpLoggingFields.All)
            .WithApiVersionSet(versionSet)
            .MapToApiVersion(2)
            .MapBatteryImageApi();

        apiGroup
            .MapGroup("/weather")
            .WithTags("Weather")
            .WithHttpLogging(HttpLoggingFields.All)
            .MapWeatherApi()
            .WithApiVersionSet(versionSet)
            .MapToApiVersion(1);

        return app;
    }
}
