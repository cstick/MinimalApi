using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using System.Threading.RateLimiting;
using Web.APIs;
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

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddHttpLogging(options =>
        {
            options.CombineLogs = true;
        });

        builder.Services.AddMemoryCache();
        builder.Logging.AddConsole();

        builder.Services.AddHostedService<StartupBackgroundService>();
        builder.Services.AddSingleton<StartupHealthCheck>();
        builder.Services.AddSingleton<ApiHealthCheck>();

        builder.Services
            .AddHealthChecks()
            .AddCheck<StartupHealthCheck>("Startup")
            .AddApiHealthCheck(
                "Cat Facts",
                new Uri("https://cat-fact.herokuapp.com/facts"),
                frequency: TimeSpan.FromSeconds(15),
                tags: ["Cat", "Dog"]);

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

        var app = builder.Build();

        app.UseHttpLogging();

        app.UseHttpsRedirection();
        //app.UseExceptionHandler();
        //app.UseAuthorization();

        app.UseRateLimiter();
        var apiGroup = app
            .MapGroup("/api")
            .RequireRateLimiting(defaultPolicyName);

        var weatherGroup = apiGroup
            .MapGroup("/weather")
            .WithTags("Weather")
            .WithHttpLogging(HttpLoggingFields.All)
            .MapWeatherApi();

        app.MapHealthChecks("/health");

        app.MapHealthChecks("/health/ready", new()
        {
            ResponseWriter = HealthResponse.Writer
        });

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.Run();
    }
}
