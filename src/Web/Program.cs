using FluentValidation;
using Microsoft.AspNetCore.HttpLogging;
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

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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

        var app = builder.Build();

        app.UseHttpLogging();

        app.UseHttpsRedirection();
        //app.UseExceptionHandler();
        //app.UseAuthorization();

        var apiGroup = app.MapGroup("/api");

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