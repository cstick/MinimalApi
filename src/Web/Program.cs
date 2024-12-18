using Asp.Versioning;
using Asp.Versioning.Conventions;
using FluentValidation;
using Microsoft.AspNetCore.HttpLogging;
using System.Threading.RateLimiting;
using Web.APIs.Groups;
using Web.Data;
using Web.Handlers;
using Web.Health;

namespace Web;

/// <summary>
/// Assembly entrypoint.
/// </summary>
public class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddHttpContextAccessor();

        builder.Services
            .AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(2);
                options.ReportApiVersions = true;
                options.ApiVersionReader = new UrlSegmentApiVersionReader();
            })
            .AddApiExplorer(options =>
            {
                options.DefaultApiVersion = new(2);
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.SubstituteApiVersionInUrl = true;
                options.GroupNameFormat = "'v'VVV";

            });

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
        });

        builder.Services.AddHttpLogging(options =>
        {
            options.CombineLogs = true;
        });

        builder.Services.AddMemoryCache();
        builder.Logging.AddConsole();

        builder.Services.AddHostedService<StartupBackgroundService>();
        builder.Services.AddSingleton<StartupHealthCheck>();
        builder.Services.AddSingleton<ApiHealthCheck>();

        builder.Services.RegisterHealthChecks();

        builder.Services.AddHttpClient();
        builder.Services.AddMediatR(configuration => configuration.RegisterServicesFromAssemblyContaining<SearchWeatherHandler>());
        builder.Services.AddValidatorsFromAssemblyContaining<Program>();

        var myOptions = new MyRateLimitOptions();
        builder.Configuration.GetSection(MyRateLimitOptions.MyRateLimit).Bind(myOptions);
        var defaultPolicyName = "default";

        builder.Services.AddRateLimiter(limiterOptions =>
        {
            limiterOptions.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
            limiterOptions.AddPolicy(policyName: defaultPolicyName, partitioner: httpContext =>
            {
                return RateLimitPartition.GetTokenBucketLimiter("Anon", _ =>
                    new TokenBucketRateLimiterOptions
                    {
                        TokenLimit = myOptions.TokenLimit,
                        QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
                        QueueLimit = myOptions.QueueLimit,
                        ReplenishmentPeriod = TimeSpan.FromSeconds(myOptions.ReplenishmentPeriod),
                        TokensPerPeriod = myOptions.TokensPerPeriod,
                        AutoReplenishment = true
                    });
            });
        });

        builder.Services.AddRepositories();

        var app = builder.Build();
        app.UseHttpLogging();
        app.UseHttpsRedirection();
        //app.UseExceptionHandler();
        //app.UseAuthorization();

        app.UseRateLimiter();

        var versionSet = app
            .NewApiVersionSet()
            .HasApiVersion(2)
            .HasDeprecatedApiVersion(1)
            .ReportApiVersions()
            .Build();

        var apiGroup = app
            .MapGroup("/product/v{version:apiVersion}")
            .RequireRateLimiting(defaultPolicyName);

        apiGroup
            .MapGroup("/batteries")
            .WithTags("Batteries")
            .WithHttpLogging(HttpLoggingFields.All)
            .MapBatteryApi()
            .WithApiVersionSet(versionSet)
            .MapToApiVersion(2);

        apiGroup
            .MapGroup("/weather")
            .WithTags("Weather")
            .WithHttpLogging(HttpLoggingFields.All)
            .MapWeatherApi()
            .WithApiVersionSet(versionSet)
            .MapToApiVersion(1)
            .MapToApiVersion(2);

        app.MapProductHealthChecks();

        // Swagger UI.
        if (app.Environment.IsDevelopment() || app.Environment.IsEnvironment("NonDevelopment"))
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
        }

        app.Run();
    }
}
